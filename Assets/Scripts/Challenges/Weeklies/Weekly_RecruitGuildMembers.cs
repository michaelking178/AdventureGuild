using UnityEngine;

public class Weekly_RecruitGuildMembers : WeeklyChallenge
{
    int[] recruitAmount = new int[] { 4, 5, 6, 8 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = recruitAmount[Random.Range(0, recruitAmount.Length)];
        Objective = $"Recruit {ObjectiveQuantity} new guild members";
        PopulationManager.OnRecruit += AddProgress;
    }

    public override void EndChallenge()
    {
        PopulationManager.OnRecruit -= AddProgress;
    }
}
