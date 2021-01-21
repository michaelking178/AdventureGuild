public class ArmourorUpgrade : Upgrade
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
        populationManager.EnableAdventurers();
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = populationManager.AdventurersEnabled;
    }
}
