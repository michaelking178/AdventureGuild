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

    private void Start()
    {
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        questPool = new List<Quest>();
        availableQuests = new List<Quest>();
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        archivedQuests = new List<Quest>();
        UpdateQuestLists();
    }

    private void UpdateQuestLists()
    {
        questPool = GetRandomAvailableQuests(6);
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

    private List<Quest> GetRandomAvailableQuests(int numOfQuests)
    {
        List<Quest> questsToGet = new List<Quest>();
        for (int i = 0; i < numOfQuests; i++)
        {
            questsToGet.Add(quests.GetRandomQuest());
        }
        return questsToGet;
    }

    public List<Quest> GetAvailableQuests()
    {
        return availableQuests;
    }

    public List<Quest> GetActiveQuests()
    {
        return activeQuests;
    }

    public List<Quest> GetCompletedQuests()
    {
        return completedQuests;
    }

    public List<Quest> GetArchivedQuests()
    {
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
    }

    public void CompleteQuest(Quest quest)
    {
        // Notify the player that a quest has completed
        quest.State = Quest.Status.Completed;
        quest.GuildMember.IsAvailable(true);
        Debug.Log(string.Format("{0} has finished!", quest.questName));
    }

    public void ArchiveQuest(Quest quest)
    {
        // Apply experience to Adventurer
        // Apply experience to all other Guild Members
        // Add quest rewards to Guildhall
        quest.State = Quest.Status.Archived;
    }
}
