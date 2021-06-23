using System.Collections.Generic;

[System.Serializable]
public class TrophyManagerData
{
    public List<Trophy> Trophies;

    public TrophyManagerData(TrophyManager trophyManager)
    {
        Trophies = trophyManager.Trophies;
    }
}
