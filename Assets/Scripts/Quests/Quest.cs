using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public int TimeToComplete { get; set; }
    public QuestReward Reward { get; set; }

    public Quest(string _questName, string _description, int _difficulty)
    {
        QuestName = _questName;
        Description = _description;
        Difficulty = _difficulty;
        GenerateQuestAttributes();
    }

    private void GenerateQuestAttributes()
    {
        TimeToComplete = 60;
        QuestReward reward = new QuestReward(Difficulty);
        Reward = reward;
    }
}
