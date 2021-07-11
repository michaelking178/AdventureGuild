public class AdventurerRecoveryBoost : Boost
{
    public override void Apply()
    {
        foreach (var adventurer in FindObjectOfType<PopulationManager>().GetAvailableAdventurers())
        {
            adventurer.Hitpoints = adventurer.MaxHitpoints;
        }
    }

    protected override void SetBoostBool(bool value)
    {
        // This boost doesn't need to do anything here.
    }

    protected override bool GetBoostBool()
    {
        // This boost doesn't need to do anything here.
        return false;
    }
}
