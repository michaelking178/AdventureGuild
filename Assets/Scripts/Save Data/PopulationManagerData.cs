[System.Serializable]
public class PopulationManagerData
{
    public System.DateTime recoveryStartTime;
    public int populationCap;
    public bool artisansEnabled;

    public PopulationManagerData(PopulationManager populationManager)
    {
        recoveryStartTime = populationManager.RecoveryStartTime;
        populationCap = populationManager.PopulationCap;
        artisansEnabled = populationManager.ArtisansEnabled;
    }
}
