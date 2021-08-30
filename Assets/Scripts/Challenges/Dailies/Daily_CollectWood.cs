using UnityEngine;

public class Daily_CollectWood : DailyChallenge
{
    private int[] woodAmounts = new int[] { 250, 500, 750, 1000 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = woodAmounts[Random.Range(0, woodAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Wood";
        Guildhall.OnWoodReward += AddProgress;
    }

    public override void EndChallenge()
    {
        Guildhall.OnWoodReward -= AddProgress;
        base.EndChallenge();
    }
}
