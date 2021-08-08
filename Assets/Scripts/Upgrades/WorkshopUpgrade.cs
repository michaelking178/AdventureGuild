public class WorkshopUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.EnableArtisans();
        base.Apply();
    }
}
