public class TreasuryUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected override void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxGold);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxGold = maxUpgrade;
        if (guildhall.MaxGold < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
