using UnityEngine;

public class Daily_EarnRenown : DailyChallenge
{
    private int[] renownAmounts = new int[] { 250, 400, 550, 700 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = renownAmounts[Random.Range(0, renownAmounts.Length)];
        Objective = $"Earn {ObjectiveQuantity} Renown";
    }
}
