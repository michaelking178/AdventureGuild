using UnityEngine;

public class Daily_CollectIron : DailyChallenge
{
    private int[] ironAmounts = new int[] { 300, 400, 500, 600 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = ironAmounts[Random.Range(0, ironAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Iron";
    }
}
