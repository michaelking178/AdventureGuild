[System.Serializable]
public class PopulationManagerData
{
    public System.DateTime recoveryStartTime;
    public int populationCap;

    public PopulationManagerData(PopulationManager populationManager)
    {
        recoveryStartTime = populationManager.recoveryStartTime;
        populationCap = populationManager.PopulationCap;
    }
}
