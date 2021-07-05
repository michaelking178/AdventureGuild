public class WorkshopUpgrade : Upgrade
{
    private new void Start()
    {
        base.Start();
    }

    private new void FixedUpdate()
    {
        if (levelManager.CurrentLevel() != "Main") return;

        base.FixedUpdate();
        if (menuManager.CurrentMenu == upgradeGuildhall)
        {
            CheckForUpgrade();
        }
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.EnableArtisans();
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = populationManager.ArtisansEnabled;
    }
}
