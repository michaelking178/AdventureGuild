using UnityEngine;

public class Daily_LevelUpAdventurers : DailyChallenge
{
    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = Random.Range(3, 6);
        Objective = $"Level up Adventurers {ObjectiveQuantity} times";
    }
}
