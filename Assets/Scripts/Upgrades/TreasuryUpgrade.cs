public class TreasuryUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxGold = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
