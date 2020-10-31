using System.Collections;
using UnityEngine;

public class Up_Barracks : Upgrade
{
    [Header("Level One Upgrade")]
    [SerializeField]
    private int levelOneGoldCost;

    [SerializeField]
    private int levelOneIronCost;

    [SerializeField]
    private int levelOneWoodCost;

    [SerializeField]
    private int levelOnePopulation = 10;

    [Header("Level Two Upgrade")]
    [SerializeField]
    private int levelTwoGoldCost;

    [SerializeField]
    private int levelTwoIronCost;

    [SerializeField]
    private int levelTwoWoodCost;

    [SerializeField]
    private int levelTwoPopulation = 25;

    [Header("Level Three Upgrade")]
    [SerializeField]
    private int levelThreeGoldCost;

    [SerializeField]
    private int levelThreeIronCost;

    [SerializeField]
    private int levelThreeWoodCost;

    [SerializeField]
    private int levelThreePopulation = 50;

    private PopulationManager populationManager;
    private MenuManager menuManager;
    private GameObject menu_UpgradeGuildhall;
    private int populationUpgrade;

    private new void Start()
    {
        base.Start();
        populationManager = FindObjectOfType<PopulationManager>();
        menuManager = FindObjectOfType<MenuManager>();
        menu_UpgradeGuildhall = FindObjectOfType<Menu_UpgradeGuildhall>().gameObject;
        StartCoroutine(DelayedCheckForUpgrade());
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == menu_UpgradeGuildhall)
        {
            CheckForUpgrade();
        }
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.SetPopulationCap(populationUpgrade);
        if (populationManager.PopulationCap < levelThreePopulation)
        {
            IsPurchased = false;
        }
    }

    private void CheckForUpgrade()
    {
        if (populationManager.PopulationCap < levelOnePopulation)
        {
            GoldCost = levelOneGoldCost;
            IronCost = levelOneIronCost;
            WoodCost = levelOneWoodCost;
            populationUpgrade = levelOnePopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks I", "Increases the maximum population of the Adventure Guild to 10.");
        }
        else if (populationManager.PopulationCap == levelOnePopulation)
        {
            GoldCost = levelTwoGoldCost;
            IronCost = levelTwoIronCost;
            WoodCost = levelTwoWoodCost;
            populationUpgrade = levelTwoPopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks II", "Increases the maximum population of the Adventure Guild to 25.");
        }
        else if (populationManager.PopulationCap == levelTwoPopulation)
        {
            GoldCost = levelThreeGoldCost;
            IronCost = levelThreeIronCost;
            WoodCost = levelThreeWoodCost;
            populationUpgrade = levelThreePopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks III", "Increases the maximum population of the Adventure Guild to 50.");
        }
        else
        {
            IsPurchased = true;
        }
    }

    private IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade();
        yield return null;
    }
}
