public class QuestWoodBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestWoodBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsQuestWoodBoosted;
    }
}
