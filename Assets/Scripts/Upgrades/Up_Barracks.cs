using UnityEngine;

public class Up_Barracks : Upgrade
{
    [SerializeField]
    private int populationUpgrade = 0;

    private PopulationManager populationManager;

    private new void Start()
    {
        base.Start();
        populationManager = FindObjectOfType<PopulationManager>();
        if (populationManager.PopulationCap == populationUpgrade)
        {
            IsPurchased = true;
        }
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.SetPopulationCap(populationUpgrade);
    }
}
