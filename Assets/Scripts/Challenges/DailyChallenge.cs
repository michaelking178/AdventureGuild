public class DailyChallenge : Challenge
{
    protected int GoldReward = 1000;
    protected int WoodReward = 500;
    protected int IronReward = 300;
    protected int RenownReward = 25;

    public virtual void Init()
    {
        Reward = new Reward(GoldReward, IronReward, WoodReward, 0, RenownReward);
    }
}
