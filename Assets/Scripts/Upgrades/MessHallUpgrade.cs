public class MessHallUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.ApplyAdventurerHPUpgrade(UpgradeTiers[NextTier()].EffectIncrement);
        base.Apply();
    }
}
