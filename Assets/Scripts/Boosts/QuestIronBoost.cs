public class QuestIronBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestIronBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsQuestIronBoosted;
    }
}
