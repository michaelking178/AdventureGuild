public class RefineryUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxIron = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
