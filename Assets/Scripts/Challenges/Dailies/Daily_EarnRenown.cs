using UnityEngine;

public class Daily_EarnRenown : DailyChallenge
{
    private int[] renownAmounts = new int[] { 50, 75, 100, 125 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = renownAmounts[Random.Range(0, renownAmounts.Length)];
        Objective = $"Earn {ObjectiveQuantity} Renown";
        Guildhall.OnRenownReward += AddProgress;
    }

    public override void EndChallenge()
    {
        Guildhall.OnRenownReward -= AddProgress;
        base.EndChallenge();
    }
}
