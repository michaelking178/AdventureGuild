public class ArmourorUpgrade : Upgrade
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
        populationManager.EnableAdventurers();
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = populationManager.AdventurersEnabled;
    }
}
