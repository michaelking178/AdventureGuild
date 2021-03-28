using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private QuestTimer questTimerPrefab;

    [SerializeField]
    private TextAsset questsJson;

    // questPool is the actual list of quests that are in use.
    [SerializeField]
    private List<Quest> questPool;          

    [SerializeField]
    private List<Quest> questArchive;

    public bool CombatUnlocked { get; set; }
    public bool EspionageUnlocked { get; set; }
    public bool DiplomacyUnlocked { get; set; }
    public Quest CurrentQuest { get; set; }
    public int QuestsCompleted { get; private set; }

    // quests is just a reference to the entire Quest JSON list. It shouldn't be used directly.
    private Quests quests;                  
    private IncidentManager incidentManager;
    private LevelManager levelManager;
    private Guildhall guildhall;
    private NotificationManager notificationManager;
    private BoostManager boostManager;
    private readonly string failureMessage = "The challenges were too great, and I was defeated before completing my quest. I have returned to the Adventure Guild so that I may recover.";
    private string rewardMessage = "";
    private int questArchiveCap = 100;

    private void Start()
    {
        incidentManager = FindObjectOfType<IncidentManager>();
        levelManager = FindObjectOfType<LevelManager>();
        guildhall = FindObjectOfType<Guildhall>();
        notificationManager = FindObjectOfType<NotificationManager>();
        boostManager = FindObjectOfType<BoostManager>();
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        if (questPool == null)
            questPool = new List<Quest>();
        if (questArchive == null)
            questArchive = new List<Quest>();
        if (!SaveSystem.SaveFileExists() && questPool.Count == 0)
            PopulateQuestPool(4);
    }

    private void FixedUpdate()
    {
        CullQuestArchive();
        if (levelManager.CurrentLevel() == "Title") return;

        if (GetQuestsByStatus(Quest.Status.New).Count <= 3)
            PopulateQuestPool(UnityEngine.Random.Range(3,6));
    }

    private void CullQuestArchive()
    {
        if (questArchive.Count > questArchiveCap)
        {
            SortQuestArchiveByStartTime();
            questArchive.RemoveRange(questArchiveCap, questArchive.Count - questArchiveCap);
        }
    }

    public void PopulateQuestPool(int numOfQuests)
    {
        int maxLevel = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>().Level;
        int breakout = 0;

        // Protect against infinite loop caused by trying to add more quests than exist in the JSON file.
        if (numOfQuests > 5)
            numOfQuests = 5;
        if (questPool.Count + numOfQuests < 15)
        {
            List<Quest> questsToGet = new List<Quest>();
            for (int i = 0; i < numOfQuests; i++)
            {
                Quest questToClone;
                do
                {
                    breakout++;
                    questToClone = quests.GetRandomQuest();
                }
                while (!Helpers.IsUniqueMember(questToClone.questName, questsToGet.Select(q => q.questName).ToList())
                        || !Helpers.IsUniqueMember(questToClone.questName, questPool.Select(q => q.questName).ToList())
                        || questToClone.level > maxLevel + 1
                        || questToClone.continuingQuest
                        || breakout < 50);
                Quest quest = CloneQuest(questToClone);
                questsToGet.Add(quest);
            }
            foreach (Quest quest in questsToGet)
            {
                questPool.Add(quest);
            }
            SortQuestPoolByStartTime();
        }
    }

    public Quest GetQuestById(int _id)
    {
        foreach (Quest quest in questPool)
        {
            if (quest.questInstanceId == _id)
                return quest;
        }
        Debug.Log("QuestManager.cs GetQuestById() could not find the requested Quest: " + _id);
        return null;
    }

    public List<Quest> GetQuestsByStatus(Quest.Status _status)
    {
        List<Quest> questList = new List<Quest>();
        foreach (Quest quest in questPool)
        {
            if (quest.State == _status)
                questList.Add(quest);
        }
        return questList;
    }

    public void SetAdventurer(GuildMember guildMember)
    {
        CurrentQuest.GuildMember = guildMember;
    }

    public void StartQuest()
    {
        QuestTimer questTimer = Instantiate(questTimerPrefab, transform);
        questTimer.SetQuest(CurrentQuest);
        questTimer.StartTimer();
        CurrentQuest.GuildMember.IsAvailable = false;
        CurrentQuest.startTime = DateTime.Now;
        CurrentQuest.State = Quest.Status.Active;
        CurrentQuest.Timer = questTimer;
        SetBoostBools();
    }

    public void CompleteQuest(Quest quest)
    {
        notificationManager.CreateNotification(string.Format("The quest \"{0}\" has completed!", quest.questName), Notification.Spirit.Good);
        quest.State = Quest.Status.Completed;
        rewardMessage = "Quest Completed!";
        quest.Incidents.Add(incidentManager.CreateCustomIncident(quest.completion, Incident.Result.Good, rewardMessage, DateTime.Now));
        ApplyQuestReward(quest);
        quest.GuildMember.IsAvailable= true;
        questArchive.Add(quest);
        questPool.Remove(quest);
        if (quest.questChain)
            AddQuestToPool(quest);

        SortQuestPoolByStartTime();
        SortQuestArchiveByStartTime();
        QuestsCompleted++;
    }

    public void FailQuest(Quest quest)
    {
        notificationManager.CreateNotification(string.Format("The quest \"{0}\" has failed!", quest.questName), Notification.Spirit.Bad);
        quest.State = Quest.Status.Failed;
        rewardMessage = "Quest Failed!";
        quest.Incidents.Add(incidentManager.CreateCustomIncident(failureMessage, Incident.Result.Bad, rewardMessage));
        quest.GuildMember.IsAvailable = true;
        questArchive.Add(quest);
        questPool.Remove(quest);
        SortQuestPoolByStartTime();
        SortQuestArchiveByStartTime();
        QuestsCompleted++;
    }

    public void ApplyQuestReward(Quest quest)
    {
        // Todo: ExpBoost debug can be removed later.
        if (FindObjectOfType<PopulationManager>().DebugBoostEnabled)
        {
            quest.Reward.Gold *= FindObjectOfType<PopulationManager>().DebugBoost;
            quest.Reward.Iron *= FindObjectOfType<PopulationManager>().DebugBoost;
            quest.Reward.Wood *= FindObjectOfType<PopulationManager>().DebugBoost;
            quest.Reward.Renown *= FindObjectOfType<PopulationManager>().DebugBoost;
        }

        guildhall.AdjustGold(quest.Reward.Gold);
        guildhall.AdjustIron(quest.Reward.Iron);
        guildhall.AdjustWood(quest.Reward.Wood);
        guildhall.AdjustRenown(quest.Reward.Renown);
        quest.GuildMember.AddExp(quest.Reward.Exp);
        quest.GuildMember.AddExp(quest.QuestSkill, quest.Reward.SkillExp);
        ApplyBoostReward(quest);
    }

    public List<Quest> GetQuestPool()
    {
        return questPool;
    }

    public List<Quest> GetQuestArchive()
    {
        return questArchive;
    }

    public void SetQuestPool(List<Quest> questList)
    {
        questPool.Clear();
        questPool = questList;
    }

    public void SetQuestArchive(List<Quest> questList)
    {
        questArchive.Clear();
        questArchive = questList;
    }

    public void LoadQuestTimer(QuestTimerData questTimerData)
    {
        QuestTimer questTimer = Instantiate(questTimerPrefab, transform);
        questTimer.SetQuest(FindQuestById(questTimerData.questInstanceId));
        if (questTimer.GetQuest() == null)
        {
            Debug.Log("QuestManager LoadQuestTimer() could not find the Quest by ID!");
        }
        else
        {
            questTimer.TimeLimit = questTimerData.timeLimit;
            questTimer.StartTime = questTimerData.startTime;
            questTimer.IsTiming = questTimerData.isTiming;
            questTimer.IncidentTimer = questTimerData.incidentTimer;
            questTimer.GetQuest().Timer = questTimer;
        }
    }

    public void SortQuestPoolByLevel()
    {
        List<Quest> sorted = questPool.OrderBy(quest => quest.level).ToList();
        questPool = sorted;
    }

    public void SortQuestPoolByStartTime()
    {
        List<Quest> sorted = questPool.OrderByDescending(quest => quest.startTime).ToList();
        questPool = sorted;
    }

        public void SortQuestArchiveByStartTime()
    {
        List<Quest> sorted = questArchive.OrderByDescending(quest => quest.startTime).ToList();
        questArchive = sorted;
    }

    public void UnlockSkill(string _skill)
    {
        switch (_skill)
        {
            case "Combat":
                CombatUnlocked = true;
                break;
            case "Espionage":
                EspionageUnlocked = true;
                break;
            case "Diplomacy":
                DiplomacyUnlocked = true;
                break;
            default:
                Debug.Log("Cannot unlock skill: " + _skill);
                break;
        }
    }

    public bool IsSkillUnlocked(string _skill)
    {
        switch (_skill)
        {
            case "Combat":
                return CombatUnlocked;
            case "Espionage":
                return EspionageUnlocked;
            case "Diplomacy":
                return DiplomacyUnlocked;
            default:
                Debug.Log("Cannot find skill: " + _skill);
                return false;
        }
    }

    private Quest CloneQuest(Quest questToClone)
    {
        Quest quest = new Quest
        {
            questName = questToClone.questName,
            skill = questToClone.skill,
            faction = questToClone.faction,
            description = questToClone.description,
            commencement = questToClone.commencement,
            completion = questToClone.completion,
            id = questToClone.id,
            continuingQuest = questToClone.continuingQuest,
            questChain = questToClone.questChain,
            nextQuestID = questToClone.nextQuestID,
            level = questToClone.level
        };

        if (questToClone.contractor == "")
        {
            string gender;

            int genderRoll = UnityEngine.Random.Range(0, 2);
            if (genderRoll == 0)
                gender = "male";
            else
                gender = "female";
            quest.contractor = FindObjectOfType<NameGenerator>().FullName(gender);
        }
        else
            quest.contractor = questToClone.contractor;

        quest.Init();
        return quest;
    }

    private Quest FindQuestById(int _id)
    {
        foreach (Quest quest in questPool)
        {
            if (quest.questInstanceId == _id)
                return quest;
        }
        foreach (Quest quest in questArchive)
        {
            if (quest.questInstanceId == _id)
                return quest;
        }
        Debug.Log("FindQuestById() did not return a Quest!");
        return null;
    }

    private void AddQuestToPool(Quest previousQuest)
    {
        Quest quest = CloneQuest(quests.GetQuestById(previousQuest.nextQuestID));
        questPool.Add(quest);
        SortQuestPoolByStartTime();
    }

    private void SetBoostBools()
    {
        if (boostManager.IsQuestExpBoosted)
            CurrentQuest.ExpBoosted = true;
        if (boostManager.IsQuestGoldBoosted)
            CurrentQuest.GoldBoosted = true;
        if (boostManager.IsQuestWoodBoosted)
            CurrentQuest.WoodBoosted = true;
        if (boostManager.IsQuestIronBoosted)
            CurrentQuest.IronBoosted = true;
    }

    private void ApplyBoostReward(Quest quest)
    {
        if (quest.ExpBoosted)
            quest.GuildMember.AddExp(quest.Reward.BoostExp);
        if (quest.GoldBoosted)
            guildhall.AdjustGold(quest.Reward.BoostGold);
        if (quest.WoodBoosted)
            guildhall.AdjustWood(quest.Reward.BoostWood);
        if (quest.IronBoosted)
            guildhall.AdjustIron(quest.Reward.BoostIron);
    }
}
