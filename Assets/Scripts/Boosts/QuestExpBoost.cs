public class QuestExpBoost : Boost
{
    protected override void SetBoostBool(bool value)
    {
        boostManager.IsQuestExpBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsQuestExpBoosted;
    }
}
