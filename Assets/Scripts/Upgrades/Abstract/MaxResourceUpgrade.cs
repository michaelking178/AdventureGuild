using UnityEngine;

public class MaxResourceUpgrade : Upgrade
{
    [SerializeField]
    protected int LevelOneUpgrade;

    #region Level Two
    [Header("Level Two")]
    [SerializeField]
    protected string LevelTwoName;

    [SerializeField]
    protected string LevelTwoDescription;

    [SerializeField]
    protected int LevelTwoGoldCost;

    [SerializeField]
    protected int LevelTwoIronCost;

    [SerializeField]
    protected int LevelTwoWoodCost;

    [SerializeField]
    protected int LevelTwoArtisanCost;

    [SerializeField]
    protected int LevelTwoConstructionTime;

    [SerializeField]
    protected int LevelTwoExperience;

    [SerializeField]
    protected int LevelTwoUpgrade;
    #endregion

    #region Level Three
    [Header("Level Three")]
    [SerializeField]
    protected string LevelThreeName;

    [SerializeField]
    protected string LevelThreeDescription;

    [SerializeField]
    protected int LevelThreeGoldCost;

    [SerializeField]
    protected int LevelThreeIronCost;

    [SerializeField]
    protected int LevelThreeWoodCost;

    [SerializeField]
    protected int LevelThreeArtisanCost;

    [SerializeField]
    protected int LevelThreeConstructionTime;

    [SerializeField]
    protected int LevelThreeExperience;

    [SerializeField]
    protected int LevelThreeUpgrade;
    #endregion

    #region Level Four
    [Header("Level Four")]
    [SerializeField]
    protected string LevelFourName;

    [SerializeField]
    protected string LevelFourDescription;

    [SerializeField]
    protected int LevelFourGoldCost;

    [SerializeField]
    protected int LevelFourIronCost;

    [SerializeField]
    protected int LevelFourWoodCost;

    [SerializeField]
    protected int LevelFourArtisanCost;

    [SerializeField]
    protected int LevelFourConstructionTime;

    [SerializeField]
    protected int LevelFourExperience;

    [SerializeField]
    protected int LevelFourUpgrade;
    #endregion

    #region Level Five
    [Header("Level Five")]
    [SerializeField]
    protected string LevelFiveName;

    [SerializeField]
    protected string LevelFiveDescription;

    [SerializeField]
    protected int LevelFiveGoldCost;

    [SerializeField]
    protected int LevelFiveIronCost;

    [SerializeField]
    protected int LevelFiveWoodCost;

    [SerializeField]
    protected int LevelFiveArtisanCost;

    [SerializeField]
    protected int LevelFiveConstructionTime;

    [SerializeField]
    protected int LevelFiveExperience;

    [SerializeField]
    protected int LevelFiveUpgrade;
    #endregion

    protected int maxUpgrade = 0;

    private new void Start()
    {
        base.Start();
    }

    public override void Apply()
    {
        base.Apply();
    }

    protected void CheckForUpgrade(int currentMax)
    {
        if (currentMax < LevelOneUpgrade)
        {
            maxUpgrade = LevelOneUpgrade;
        }
        else if (currentMax < LevelTwoUpgrade)
        {
            maxUpgrade = LevelTwoUpgrade;
            GoldCost = LevelTwoGoldCost;
            IronCost = LevelTwoIronCost;
            WoodCost = LevelTwoWoodCost;
            ArtisanCost = LevelTwoArtisanCost;
            Experience = LevelTwoExperience;
            constructionTime = LevelTwoConstructionTime;
            Name = LevelTwoName;
            Description = LevelTwoDescription;
        }
        else if (currentMax < LevelThreeUpgrade)
        {
            maxUpgrade = LevelThreeUpgrade;
            GoldCost = LevelThreeGoldCost;
            IronCost = LevelThreeIronCost;
            WoodCost = LevelThreeWoodCost;
            ArtisanCost = LevelThreeArtisanCost;
            Experience = LevelThreeExperience;
            constructionTime = LevelThreeConstructionTime;
            Name = LevelThreeName;
            Description = LevelThreeDescription;
        }
        else if (currentMax < LevelFourUpgrade)
        {
            maxUpgrade = LevelFourUpgrade;
            GoldCost = LevelFourGoldCost;
            IronCost = LevelFourIronCost;
            WoodCost = LevelFourWoodCost;
            ArtisanCost = LevelFourArtisanCost;
            Experience = LevelFourExperience;
            constructionTime = LevelFourConstructionTime;
            Name = LevelFourName;
            Description = LevelFourDescription;
        }
        else
        {
            maxUpgrade = LevelFiveUpgrade;
            GoldCost = LevelFiveGoldCost;
            IronCost = LevelFiveIronCost;
            WoodCost = LevelFiveWoodCost;
            ArtisanCost = LevelFiveArtisanCost;
            Experience = LevelFiveExperience;
            constructionTime = LevelFiveConstructionTime;
            Name = LevelFiveName;
            Description = LevelFiveDescription;
        }
    }
}
