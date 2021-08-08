public class MerchantUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxGoldIncome = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
