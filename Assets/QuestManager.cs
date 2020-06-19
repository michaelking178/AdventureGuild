using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private List<Quest> questList = new List<Quest>();

    private void Start()
    {
        CreateQuest(0);

    }

    public void CreateQuest(int difficulty)
    {
        string questName = "Heir today, gone tomorrow";
        string description = "Explore the wildlands and discover the fate of the Prince's caravan.";

        Quest newQuest = new Quest(questName, description, difficulty);

        Debug.Log("\n" + newQuest.QuestName + ":\n" + newQuest.Description + "\n"
    + "Rewards:\n"
    + newQuest.Reward.Gold + " Gold\n"
    + newQuest.Reward.Iron + " Iron\n"
    + newQuest.Reward.Wood + " Wood\n"
    + newQuest.Reward.Exp + " Experience");

        questList.Add(newQuest);
    }
}
