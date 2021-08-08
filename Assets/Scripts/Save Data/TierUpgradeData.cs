[System.Serializable]
public class TierUpgradeData
{
    public string Name = "";
    public int CurrentTier = -1;

    public TierUpgradeData(string upgradeObjectName, int current)
    {
        Name = upgradeObjectName;
        CurrentTier = current;
    }
}
