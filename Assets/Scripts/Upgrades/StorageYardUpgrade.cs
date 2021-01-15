public class StorageYardUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    protected override void CheckForUpgrade()
    {
        CheckForUpgrade(guildhall.MaxWood);
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxWood = maxUpgrade;
        if (guildhall.MaxWood < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
