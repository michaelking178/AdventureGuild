public class QuestGoldBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestGoldBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsQuestGoldBoosted;
    }
}
