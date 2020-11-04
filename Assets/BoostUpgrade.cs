using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostUpgrade : Upgrade
{
    public enum BoostStat { QuestExperience, QuestDifficulty, QuestResources, TrainingExperience }
    public BoostStat Boost;

    public void ApplyBoost()
    {
        switch (Boost)
        {
            case BoostStat.QuestDifficulty:
                break;
            case BoostStat.QuestExperience:
                break;
            case BoostStat.QuestResources:
                break;
            case BoostStat.TrainingExperience:
                break;
            default:
                break;
        }
    }
}
