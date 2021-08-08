public class BarracksUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.SetPopulationCap(UpgradeTiers[NextTier()].EffectIncrement);
        base.Apply();
    }
}
