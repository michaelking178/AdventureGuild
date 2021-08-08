public class HallOfTrials : TierUpgrade
{
    public override void Apply()
    {
        FindObjectOfType<ChallengeManager>().UnlockChallenges();
        base.Apply();
    }
}
