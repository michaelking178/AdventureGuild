using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset questsJson;

    private Quests quests;
    private Quest[] availableQuests;
    private List<Quest> completedQuests; // These are quests that are finished but awaiting the user to cash in. 100% completed and cashed in quests are moved to the questArchive
    private List<Quest> questArchive;
    private Quest activeQuest;

    private void Start()
    {
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        availableQuests = GetQuests(5);
        foreach (Quest quest in availableQuests)
        {
            quest.Init();
            Debug.Log(quest.Reward.Gold);
        }
    }

    private Quest[] GetQuests(int numOfQuests)
    {
        Quest[] questsToGet = new Quest[numOfQuests];
        for (int i = 0; i < numOfQuests; i++)
        {
            questsToGet[i] = quests.GetRandomQuest();
        }
        return questsToGet;
    }

    public Quest[] AvailableQuests()
    {
        return availableQuests;
    }

    public void SetActiveQuest(Quest quest)
    {
        activeQuest = quest;
    }

    public Quest GetActiveQuest()
    {
        return activeQuest;
    }

    public void SetAdventurer(GuildMember guildMember)
    {
        activeQuest.GuildMember = guildMember;
    }
}
