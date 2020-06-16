using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Exp { get; set; }

    public QuestReward(int difficulty)
    {
        Gold = 100 * (difficulty + 1);
        Iron = 10 * (difficulty + 1);
        Wood = 25 * (difficulty + 1);
        Exp = 1000 * (difficulty + 1);
    }
}
