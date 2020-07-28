using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private QuestTimer questTimerPrefab;

    [SerializeField]
    private TextAsset questsJson;

    private Quests quests;                  // quests is just a reference to the entire Quest JSON list. It shouldn't be used directly.
    private List<Quest> questPool;          // questPool is the actual list of quests that are in use.
    private List<Quest> availableQuests;    // quests available to start
    private List<Quest> activeQuests;       // quests currently being undertaken
    private List<Quest> completedQuests;    // quests that are finished but awaiting cash in.
    private List<Quest> archivedQuests;     // quests that are cashed in and over, saved for some fun archive feature
    private Quest currentQuest;

    private IncidentManager incidentManager;
    private Guildhall guildhall;

    private void Start()
    {
        incidentManager = FindObjectOfType<IncidentManager>();
        guildhall = FindObjectOfType<Guildhall>();
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        questPool = new List<Quest>();
        availableQuests = new List<Quest>();
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        archivedQuests = new List<Quest>();

        questPool = PopulateQuestPool(6);
        UpdateQuestLists();
    }

    private void UpdateQuestLists()
    {
        availableQuests.Clear();
        activeQuests.Clear();
        completedQuests.Clear();
        archivedQuests.Clear();
        foreach (Quest quest in questPool)
        {
            if (quest.State == Quest.Status.New)
            {
                availableQuests.Add(quest);
            }
            else if (quest.State == Quest.Status.Active)
            {
                activeQuests.Add(quest);
            }
            else if (quest.State == Quest.Status.Completed)
            {
                completedQuests.Add(quest);
            }
            else if (quest.State == Quest.Status.Archived)
            {
                archivedQuests.Add(quest);
            }
        }
    }

    private List<Quest> PopulateQuestPool(int numOfQuests)
    {
        List<Quest> questsToGet = new List<Quest>();
        for (int i = 0; i < numOfQuests; i++)
        {
            Quest quest = quests.GetRandomQuest();
            if (Helpers.IsUniqueMember(quest, questsToGet))
            questsToGet.Add(quest);
        }
        return questsToGet;
    }

    public List<Quest> GetAvailableQuests()
    {
        UpdateQuestLists();
        return availableQuests;
    }

    public List<Quest> GetActiveQuests()
    {
        UpdateQuestLists();
        return activeQuests;
    }

    public List<Quest> GetCompletedQuests()
    {
        UpdateQuestLists();
        return completedQuests;
    }

    public List<Quest> GetArchivedQuests()
    {
        UpdateQuestLists();
        return archivedQuests;
    }

    public void SetCurrentQuest(Quest quest)
    {
        currentQuest = quest;
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    public void SetAdventurer(GuildMember guildMember)
    {
        currentQuest.GuildMember = guildMember;
    }

    public void StartQuest()
    {
        QuestTimer questTimer = Instantiate(questTimerPrefab, transform);
        questTimer.SetQuest(currentQuest);
        questTimer.StartTimer();
        currentQuest.GuildMember.IsAvailable(false);
        currentQuest.startTime = DateTime.Now;
        currentQuest.State = Quest.Status.Active;
        UpdateQuestLists();
    }

    public void CompleteQuest(Quest quest)
    {
        // Todo: Notify the player that a quest has completed
        quest.State = Quest.Status.Completed;
        quest.Incidents.Add(incidentManager.CreateCustomIncident(quest.completion, Incident.Result.Null, DateTime.Now));
        ApplyQuestReward(quest);
        quest.GuildMember.IsAvailable(true);
        UpdateQuestLists();
    }

    public void ArchiveQuest(Quest quest)
    {
        quest.State = Quest.Status.Archived;
        UpdateQuestLists();
    }

    public void ApplyQuestReward(Quest quest)
    {
        guildhall.ChangeGoldBy(quest.Reward.Gold);
        guildhall.ChangeIronBy(quest.Reward.Iron);
        guildhall.ChangeWoodBy(quest.Reward.Wood);
        quest.GuildMember.AddExp(quest.Reward.Exp);
    }

    public List<Quest> GetQuestPool()
    {
        return questPool;
    }
}
