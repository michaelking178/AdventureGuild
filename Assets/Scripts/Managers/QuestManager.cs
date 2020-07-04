using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset questsJson;

    private Quests quests;
    private Quest[] availableQuests;
    private Quest activeQuest;

    private void Start()
    {
        quests = JsonUtility.FromJson<Quests>(questsJson.text);
        availableQuests = GetQuests(5);
        PrintQuests();
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

    private void PrintQuests()
    {
        foreach(Quest newQuest in availableQuests)
        {
            newQuest.reward = new QuestReward(0);
            Debug.Log("\n" + newQuest.questName + ":\n" + newQuest.description + "\n"
                + "Rewards:\n"
                + newQuest.reward.Gold + " Gold\n"
                + newQuest.reward.Iron + " Iron\n"
                + newQuest.reward.Wood + " Wood\n"
                + newQuest.reward.Exp + " Experience");
        }
    }

    public void SetActiveQuest(Quest quest)
    {
        activeQuest = quest;
    }

    public Quest GetActiveQuest()
    {
        return activeQuest;
    }
}
