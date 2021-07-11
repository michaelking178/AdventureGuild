public class QuestRewardBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestRewardBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsQuestRewardBoosted;
    }
}
