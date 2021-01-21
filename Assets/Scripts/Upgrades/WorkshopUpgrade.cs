public class WorkshopUpgrade : Upgrade
{
    private new void Start()
    {
        base.Start();
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.EnableArtisans();
    }

    protected void CheckForUpgrade()
    {
        IsPurchased = FindObjectOfType<PopulationManager>().ArtisansEnabled;
    }
}
