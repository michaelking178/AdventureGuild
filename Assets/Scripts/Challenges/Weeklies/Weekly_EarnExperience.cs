using UnityEngine;

public class Weekly_EarnExperience : WeeklyChallenge
{
    private int[] expAmounts = new int[] { 15000, 20000, 25000, 30000 };

    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = expAmounts[Random.Range(0, expAmounts.Length)];
        Objective = $"Earn {ObjectiveQuantity} Experience";
        GuildMember.OnExperienceGained += AddProgress;
    }

    public override void EndChallenge()
    {
        GuildMember.OnExperienceGained -= AddProgress;
        base.EndChallenge();
    }
}
