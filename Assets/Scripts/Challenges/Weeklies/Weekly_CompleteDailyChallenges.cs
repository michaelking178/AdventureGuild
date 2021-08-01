using UnityEngine;

public class Weekly_CompleteDailyChallenges : WeeklyChallenge
{
    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = Random.Range(5, 11);
        Objective = $"Complete {ObjectiveQuantity} Daily Challenges";
        DailyChallenge.OnDailyChallengeCompleted += AddProgress;
    }

    public override void EndChallenge()
    {
        DailyChallenge.OnDailyChallengeCompleted -= AddProgress;
    }
}
