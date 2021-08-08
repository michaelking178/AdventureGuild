using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Construction : Menu
{
    #region Data

    public TierUpgrade ConstructionJob;

    [SerializeField]
    private Button buildButton;

    [SerializeField]
    private Button selectArtisansButton;

    [SerializeField]
    private TextMeshProUGUI projectText;

    [SerializeField]
    private TextMeshProUGUI goldCostText;

    [SerializeField]
    private TextMeshProUGUI woodCostText;

    [SerializeField]
    private TextMeshProUGUI ironCostText;

    [SerializeField]
    private TextMeshProUGUI artisanCostText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI expText;

    [SerializeField]
    private TextMeshProUGUI upgradeText;

    private TierUpgradeObject constructionTier;
    private Guildhall guildhall;
    private ConstructionManager constructionManager;
    private Color green = new Color(0.08977485f, 0.5566038f, 0);

    #endregion

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            Populate();
        }
    }

    public void Populate()
    {
        if (ConstructionJob != null)
        {
            constructionTier = ConstructionJob.UpgradeTiers[ConstructionJob.NextTier()];
            SetButtons();

            if (constructionTier.ArtisanCost == 0) artisanCostText.gameObject.SetActive(false);
            else artisanCostText.gameObject.SetActive(true);

            if (constructionTier.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (constructionTier.WoodCost == 0) woodCostText.gameObject.SetActive(false);
            else woodCostText.gameObject.SetActive(true);

            if (constructionTier.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (constructionTier.IronCost == 0) ironCostText.gameObject.SetActive(false);
            else ironCostText.gameObject.SetActive(true);

            projectText.text = constructionTier.Name;

            artisanCostText.text = $"Artisan Proficiency: {constructionTier.ArtisanCost}";
            SetColor(artisanCostText, guildhall.ArtisanProficiency, constructionTier.ArtisanCost);

            goldCostText.text = $"Gold: {guildhall.Gold} / {constructionTier.GoldCost}";
            SetColor(goldCostText, guildhall.Gold, constructionTier.GoldCost);

            woodCostText.text = $"Wood: {guildhall.Wood} / {constructionTier.WoodCost}";
            SetColor(woodCostText, guildhall.Wood, constructionTier.WoodCost);

            ironCostText.text = $"Iron: {guildhall.Iron} / {constructionTier.IronCost}";
            SetColor(ironCostText, guildhall.Iron, constructionTier.IronCost);

            timeText.text = Helpers.FormatTimer((int)constructionTier.constructionTime);
            expText.text = $"{constructionTier.Experience} Artisan Experience";
            upgradeText.text = constructionTier.Description;
        }
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(ConstructionJob);
        constructionManager.BeginConstruction();
        menuManager.OpenMenu(FindObjectOfType<Menu_UpgradeGuildhall>());
    }

    public void PassUpgradeToSelectArtisans()
    {
        Menu_SelectArtisans menu_SelectArtisans = FindObjectOfType<Menu_SelectArtisans>();
        menu_SelectArtisans.SetUpgrade(ConstructionJob);
        menu_SelectArtisans.Open();
    }

    private void SetColor(TextMeshProUGUI text, int have, int need)
    {
        if (have < need) text.color = Color.red;
        else text.color = green;
    }

    private void SetButtons()
    {
        if (constructionManager.UnderConstruction || !ConstructionJob.CanAfford())
        {
            selectArtisansButton.interactable = false;
            buildButton.interactable = false;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.75f);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.75f);
        }
        else
        {
            selectArtisansButton.interactable = true;
            buildButton.interactable = true;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        }

        if (constructionTier.ArtisanCost == 0)
        {
            buildButton.gameObject.SetActive(true);
            selectArtisansButton.gameObject.SetActive(false);
        }
        else
        {
            buildButton.gameObject.SetActive(false);
            selectArtisansButton.gameObject.SetActive(true);
        }
    }
}
