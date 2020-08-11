[System.Serializable]
public class PopulationManagerData
{
    public System.DateTime recoveryStartTime;

    public PopulationManagerData(PopulationManager populationManager)
    {
        recoveryStartTime = populationManager.recoveryStartTime;
    }
}
