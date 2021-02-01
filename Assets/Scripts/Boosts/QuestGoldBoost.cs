public class QuestGoldBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestIronBoosted = value;
    }
}
