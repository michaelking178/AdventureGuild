using System.Collections;
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
        StartCoroutine(CheckForUpgrade());
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.SetPopulationCap(populationUpgrade);
    }

    private IEnumerator CheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        if (populationManager.PopulationCap >= populationUpgrade)
        {
            IsPurchased = true;
        }
        yield return null;
    }
}
