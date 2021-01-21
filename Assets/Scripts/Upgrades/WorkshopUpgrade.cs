public class WorkshopUpgrade : Upgrade
{
    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>().CurrentMenu.name == "Menu_UpgradeGuildhall")
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
