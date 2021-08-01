public class WeeklyChallenge : Challenge
{
    protected int GoldReward = 5000;
    protected int WoodReward = 2000;
    protected int IronReward = 1000;
    protected int RenownReward = 150;

    public virtual void Init()
    {
        Reward = new Reward(GoldReward, IronReward, WoodReward, 0, RenownReward);
    }
}
