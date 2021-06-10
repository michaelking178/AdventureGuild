using System;

[Serializable]
public class PopulationManagerData
{
    public DateTime recoveryStartTime;
    public DateTime recruitStartTime;
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
