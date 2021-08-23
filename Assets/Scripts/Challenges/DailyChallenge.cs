public class DailyChallenge : Challenge
{
    protected int GoldReward = 1000;
    protected int WoodReward = 500;
    protected int IronReward = 300;
    protected int RenownReward = 25;

    public delegate void OnDailyChallengeAction(int value);
    public static event OnDailyChallengeAction OnDailyChallengeCompleted;

    public virtual void Init()
    {
        Reward = new Reward(GoldReward, IronReward, WoodReward, 0, RenownReward);
    }

    protected override void Complete()
    {
        base.Complete();
        FindObjectOfType<NotificationManager>().CreateNotification($"Daily Challenge completed! \nReward: {GoldReward} Gold, {WoodReward} Wood, {IronReward} Iron, {RenownReward} Renwon", Notification.Spirit.Good);
        OnDailyChallengeCompleted?.Invoke(1);
    }
}
