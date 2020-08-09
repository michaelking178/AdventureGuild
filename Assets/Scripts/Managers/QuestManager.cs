using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private QuestTimer questTimerPrefab;

    [SerializeField]
    private TextAsset questsJson;

    [SerializeField]
    private List<Quest> questPool;          // questPool is the actual list of quests that are in use.

    private Quests quests;                  // quests is just a reference to the entire Quest JSON list. It shouldn't be used directly.
    private IncidentManager incidentManager;
    private Guildhall guildhall;

    public Quest CurrentQuest { get; set; }

    private void Start()
    {
        incidentManager = FindObjectOfType<IncidentManager>();
        guildhall = FindObjectOfType<Guildhall>();
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        questPool = new List<Quest>();

        PopulateQuestPool(6);
    }

    public void PopulateQuestPool(int numOfQuests)
    {
        List<Quest> questsToGet = new List<Quest>();

        // Check that each quest added to the questsToGet list is unique
        for (int i = 0; i < numOfQuests; i++)
        {
            Quest quest = new Quest();
            Quest questToClone = quests.GetRandomQuest();
            quest.questName = questToClone.questName;
            quest.contractor = questToClone.contractor;
            quest.description = questToClone.description;
            quest.commencement = questToClone.commencement;
            quest.completion = questToClone.completion;
            quest.id = questToClone.id;
            quest.difficulty = questToClone.difficulty;
            quest.time = questToClone.time;
            quest.Init();
            if (Helpers.IsUniqueMember(quest, questsToGet))
                questsToGet.Add(quest);
        }

        // Add each quest in the questsToGet list to the questPool
        foreach (Quest quest in questsToGet)
        {
            questPool.Add(quest);
        }
    }
    
    public Quest GetQuestById(int _id)
    {
        foreach (Quest quest in questPool)
        {
            if (quest.questInstanceId == _id)
            {
                return quest;
            }
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
            {
                questList.Add(quest);
            }
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
    }

    public void CompleteQuest(Quest quest)
    {
        // Todo: Notify the player that a quest has completed
        quest.State = Quest.Status.Completed;
        quest.Incidents.Add(incidentManager.CreateCustomIncident(quest.completion, Incident.Result.Neutral, DateTime.Now));
        ApplyQuestReward(quest);
        quest.GuildMember.IsAvailable= true;
    }

    public void ArchiveQuest(Quest quest)
    {
        quest.State = Quest.Status.Archived;
    }

    public void ApplyQuestReward(Quest quest)
    {
        guildhall.AdjustGold(quest.Reward.Gold);
        guildhall.AdjustIron(quest.Reward.Iron);
        guildhall.AdjustWood(quest.Reward.Wood);
        guildhall.AdjustRenown(quest.Reward.Renown);
        quest.GuildMember.AddExp(quest.Reward.Exp);
    }

    public List<Quest> GetQuestPool()
    {
        return questPool;
    }

    public void SetQuestPool(List<Quest> questList)
    {
        questPool.Clear();
        questPool = questList;
    }

    public void LoadQuestTimer(QuestTimerData questTimerData)
    {
        QuestTimer questTimer = Instantiate(questTimerPrefab, transform);
        questTimer.SetQuest(FindQuestById(questTimerData.questInstanceId));
        questTimer.TimeLimit = questTimerData.timeLimit;
        questTimer.StartTime = questTimerData.startTime;
        questTimer.IsTiming = questTimerData.isTiming;
        questTimer.IncidentTimer = questTimerData.incidentTimer;
    }

    private Quest FindQuestById(int _id)
    {
        foreach(Quest quest in questPool)
        {
            if (quest.questInstanceId == _id)
            {
                return quest;
            }
        }
        Debug.Log("FindQuestById() did not return a Quest!");
        return null;
    }
}
