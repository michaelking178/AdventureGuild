using UnityEngine;

public class Daily_CollectGold : DailyChallenge
{
    private int[] goldAmounts = new int[] { 3000, 4000, 5000, 6000 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = goldAmounts[Random.Range(0, goldAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Gold";
    }
}
