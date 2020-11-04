using System.Collections;
using UnityEngine;

public class BarracksUpgrade : Upgrade
{
    [Header("Level One Upgrade")]
    // Costs inherited from Upgrade

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
    private int levelTwoArtisanCost;

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
    private int levelThreeArtisanCost;

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
            populationUpgrade = levelOnePopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks I", "Increases the maximum population of the Adventure Guild to 10.");
        }
        else if (populationManager.PopulationCap == levelOnePopulation)
        {
            base.GoldCost = levelTwoGoldCost;
            base.IronCost = levelTwoIronCost;
            base.WoodCost = levelTwoWoodCost;
            base.ArtisanCost = levelTwoArtisanCost;
            populationUpgrade = levelTwoPopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks II", "Increases the maximum population of the Adventure Guild to 25.");
        }
        else if (populationManager.PopulationCap == levelTwoPopulation)
        {
            base.GoldCost = levelThreeGoldCost;
            base.IronCost = levelThreeIronCost;
            base.WoodCost = levelThreeWoodCost;
            base.ArtisanCost = levelThreeArtisanCost;
            populationUpgrade = levelThreePopulation;
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks III", "Increases the maximum population of the Adventure Guild to 50.");
        }
        else
        {
            GetComponent<UpgradeItemFrame>().SetItemAttributes("Barracks III", "Increases the maximum population of the Adventure Guild to 50.");
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
