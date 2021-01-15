public class RefineryUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected override void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxIron);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxIron = maxUpgrade;
        if (guildhall.MaxIron < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
