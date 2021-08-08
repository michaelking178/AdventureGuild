public class LumberCampUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxWoodIncome = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
