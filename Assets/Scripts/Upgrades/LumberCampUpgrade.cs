public class LumberCampUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected override void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxWoodIncome);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxWoodIncome = maxUpgrade;
        if (guildhall.MaxWoodIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
