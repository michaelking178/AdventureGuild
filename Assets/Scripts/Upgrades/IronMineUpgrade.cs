public class IronMineUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxIronIncome = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
