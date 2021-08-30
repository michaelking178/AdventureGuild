public class BlacksmithUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.ApplyPeasantIncomeUpgrade(UpgradeTiers[NextTier()].EffectIncrement);
        base.Apply();
    }
}
