using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    [SerializeField]
    private int difficulty = 0;

    private void Start()
    {
        Quest quest = GenerateQuest(difficulty);

        Debug.Log("\n" + quest.QuestName + ":\n" + quest.Description + "\n"
            + "Rewards:\n"
            + quest.Reward.Gold + " Gold\n"
            + quest.Reward.Iron + " Iron\n"
            + quest.Reward.Wood + " Wood\n"
            + quest.Reward.Exp + " Experience");
    }

    public Quest GenerateQuest(int difficulty)
    {
        /*
         * Difficulty:
         * 0 = Easy
         * 1 = Medium
         * 2 = Hard
        */
        string questName = "Heir today, gone tomorrow";
        string description = "Explore the wildlands and discover the fate of the Prince's caravan.";
        
        Quest newQuest = new Quest(questName, description, difficulty);

        return newQuest;
    }
}
