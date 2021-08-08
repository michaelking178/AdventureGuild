public class ArmourorUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.EnableAdventurers();
        base.Apply();
    }
}
