using UnityEngine;

public class Up_Barracks : MonoBehaviour, IUpgrade
{
    [SerializeField]
    private int populationUpgrade = 0;

    public void Apply()
    {
        FindObjectOfType<Guildhall>().SetPopulationCap(populationUpgrade);
    }
}
