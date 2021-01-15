public class ArmourorUpgrade : Upgrade
{
    private new void Start()
    {
        base.Start();
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.EnableAdventurers();
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = FindObjectOfType<PopulationManager>().AdventurersEnabled;
    }
}
