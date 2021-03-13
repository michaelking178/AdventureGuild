using System;
using System.Collections.Generic;

[Serializable]
public class ConstructionManagerData
{
    public string ConstructionJobName;
    public bool UnderConstruction;
    public DateTime StartTime;
    public List<int> ArtisanIDs = new List<int>();

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

        foreach (GuildMember artisan in constructionManager.Artisans)
        {
            ArtisanIDs.Add(artisan.Id);
        }
    }
}
