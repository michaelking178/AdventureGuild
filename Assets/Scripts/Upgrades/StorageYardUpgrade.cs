public class StorageYardUpgrade : TierUpgrade
{
    public override void Apply()
    {
        guildhall.MaxWood = UpgradeTiers[NextTier()].EffectIncrement;
        base.Apply();
    }
}
