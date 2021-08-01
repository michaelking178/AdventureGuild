using UnityEngine;

public class Daily_CollectIron : DailyChallenge
{
    private int[] ironAmounts = new int[] { 150, 300, 450, 600 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = ironAmounts[Random.Range(0, ironAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Iron";
        Guildhall.OnIronReward += AddProgress;
    }

    public override void EndChallenge()
    {
        Guildhall.OnIronReward -= AddProgress;
    }
}
