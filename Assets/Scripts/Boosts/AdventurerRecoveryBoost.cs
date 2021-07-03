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
    }

    protected override bool GetBoostBool()
    {
        return false;
    }
}
