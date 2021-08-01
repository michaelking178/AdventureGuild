using UnityEngine;

public class Daily_CollectWood : DailyChallenge
{
    private int[] woodAmounts = new int[] { 500, 750, 1000, 1250 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = woodAmounts[Random.Range(0, woodAmounts.Length)];
        Objective = $"Collect {ObjectiveQuantity} Wood";
    }
}
