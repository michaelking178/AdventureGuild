using UnityEngine;

public class Daily_CollectGold : DailyChallenge
{
    private int[] goldAmounts = new int[] { 1250, 1500, 1750, 2000 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = goldAmounts[Random.Range(0, goldAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Gold";
        Guildhall.OnGoldReward += AddProgress;
    }

    public override void EndChallenge()
    {
        Guildhall.OnGoldReward -= AddProgress;
        base.EndChallenge();
    }
}
