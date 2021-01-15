[System.Serializable]
public class PopulationManagerData
{
    public System.DateTime recoveryStartTime;
    public System.DateTime recruitStartTime;
    public int populationCap;
    public bool adventurersEnabled;
    public bool artisansEnabled;

    public PopulationManagerData(PopulationManager populationManager)
    {
        recoveryStartTime = populationManager.RecoveryStartTime;
        recruitStartTime = populationManager.RecruitStartTime;
        populationCap = populationManager.PopulationCap;
        adventurersEnabled = populationManager.AdventurersEnabled;
        artisansEnabled = populationManager.ArtisansEnabled;
    }
}
