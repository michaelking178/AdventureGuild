using System;
using System.Collections.Generic;

[Serializable]
public class ConstructionManagerData
{
    public string ConstructionJobName;
    public bool UnderConstruction;
    public DateTime StartTime;
    public List<int> ArtisanIDs = new List<int>();
    public List<TierUpgradeData> TierUpgradeDatas = new List<TierUpgradeData>();

    public ConstructionManagerData(ConstructionManager constructionManager)
    {
        if (constructionManager.ConstructionJob != null)
        {
            ConstructionJobName = constructionManager.ConstructionName;
        }
        else
        {
            ConstructionJobName = "";
        }
        UnderConstruction = constructionManager.UnderConstruction;
        StartTime = constructionManager.StartTime;

        foreach(TierUpgrade upgrade in constructionManager.GetComponentsInChildren<TierUpgrade>())
        {
            TierUpgradeData upgradeData = new TierUpgradeData(upgrade.gameObject.name, upgrade.CurrentTier);
            TierUpgradeDatas.Add(upgradeData);
        }

        foreach (GuildMember artisan in constructionManager.Artisans)
        {
            ArtisanIDs.Add(artisan.Id);
        }
    }
}
