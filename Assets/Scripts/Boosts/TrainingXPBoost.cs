public class TrainingXPBoost : Boost
{
    public override void Apply()
    {
        SetBoostBool(true);
    }

    public void EndBoost()
    {
        SetBoostBool(false);
    }

    protected override void SetBoostBool(bool value)
    {
        boostManager.IsTrainingExpBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsTrainingExpBoosted;
    }
}
