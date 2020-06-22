using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset questsJson;

    [SerializeField]
    private Quests questList;

    private void Start()
    {
        questList = JsonUtility.FromJson<Quests>(questsJson.text);
        PrintQuests();
    }

    public Quest[] GetQuests()
    {
        return questList.quests;
    }

    private void PrintQuests()
    {
        foreach(Quest newQuest in questList.quests)
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
}
