public class QuestExpBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestIronBoosted = value;
    }
}
