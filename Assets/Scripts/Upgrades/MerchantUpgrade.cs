public class MerchantUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxGoldIncome);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxGoldIncome = maxUpgrade;
        if (guildhall.MaxGoldIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
