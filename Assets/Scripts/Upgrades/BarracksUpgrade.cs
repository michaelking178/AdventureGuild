using System.Collections;
using UnityEngine;

public class BarracksUpgrade : Upgrade
{
    [Header("Level One Upgrade")]
    // Level 1 Name, Description, Costs and Time inherited from Upgrade

    [SerializeField]
    private int levelOnePopulation = 10;


    [Header("Level Two Upgrade")]
    [SerializeField]
    private string levelTwoName;

    [SerializeField]
    private string levelTwoDescription;

    [SerializeField]
    private int levelTwoGoldCost;

    [SerializeField]
    private int levelTwoIronCost;

    [SerializeField]
    private int levelTwoWoodCost;

    [SerializeField]
    private int levelTwoArtisanCost;

    [SerializeField]
    private float levelTwoConstructionTime;

    [SerializeField]
    private int levelTwoExperience;

    [SerializeField]
    private int levelTwoPopulation = 25;


    [Header("Level Three Upgrade")]
    [SerializeField]
    private string levelThreeName;

    [SerializeField]
    private string levelThreeDescription;

    [SerializeField]
    private int levelThreeGoldCost;

    [SerializeField]
    private int levelThreeIronCost;

    [SerializeField]
    private int levelThreeWoodCost;

    [SerializeField]
    private int levelThreeArtisanCost;

    [SerializeField]
    private float levelThreeConstructionTime;

    [SerializeField]
    private int levelThreeExperience;

    [SerializeField]
    private int levelThreePopulation = 50;

    private MenuManager menuManager;
    private GameObject menu_UpgradeGuildhall;
    private int populationUpgrade = 10;

    private new void Start()
    {
        base.Start();
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
        Debug.Log("Applying Barracks Upgrade");
        CheckForUpgrade();
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
        }
        else if (populationManager.PopulationCap == levelOnePopulation)
        {
            GoldCost = levelTwoGoldCost;
            IronCost = levelTwoIronCost;
            WoodCost = levelTwoWoodCost;
            ArtisanCost = levelTwoArtisanCost;
            Experience = levelTwoExperience;
            populationUpgrade = levelTwoPopulation;
            constructionTime = levelTwoConstructionTime;
            Name = levelTwoName;
            Description = levelTwoDescription;
        }
        else if (populationManager.PopulationCap == levelTwoPopulation)
        {
            GoldCost = levelThreeGoldCost;
            IronCost = levelThreeIronCost;
            WoodCost = levelThreeWoodCost;
            ArtisanCost = levelThreeArtisanCost;
            Experience = levelThreeExperience;
            populationUpgrade = levelThreePopulation;
            constructionTime = levelThreeConstructionTime;
            Name = levelThreeName;
            Description = levelThreeDescription;
        }
        else
        {
            Name = levelThreeName;
            Description = levelThreeDescription;
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
