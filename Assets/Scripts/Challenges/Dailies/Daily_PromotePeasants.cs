using UnityEngine;

public class Daily_PromotePeasants : DailyChallenge
{
    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = Random.Range(3, 6);
        Objective = $"Promote {ObjectiveQuantity} peasants to either Adventurers or Artisans";
    }
}
