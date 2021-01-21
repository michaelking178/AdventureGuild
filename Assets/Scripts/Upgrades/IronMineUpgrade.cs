public class IronMineUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxIronIncome);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxIronIncome = maxUpgrade;
        if (guildhall.MaxIronIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
