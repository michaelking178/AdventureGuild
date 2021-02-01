public class QuestWoodBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestIronBoosted = value;
    }
}
