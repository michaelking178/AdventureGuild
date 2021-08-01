using UnityEngine;

public class Weekly_CompleteQuests : WeeklyChallenge
{
    int[] questsAmount = new int[] { 20, 25, 30 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = questsAmount[Random.Range(0, questsAmount.Length)];
        Objective = $"Complete {ObjectiveQuantity} quests";
    }
}
