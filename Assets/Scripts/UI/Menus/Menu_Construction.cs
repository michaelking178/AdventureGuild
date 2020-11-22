using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Construction : MonoBehaviour
{
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

    private Guildhall guildhall;
    private MenuManager menuManager;
    private ConstructionManager constructionManager;

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        menuManager = FindObjectOfType<MenuManager>();
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            Populate();
        }
    }

    public void Populate()
    {
        if (constructionManager.ConstructionJob != null)
        {
            SetButtons(constructionManager.ConstructionJob);

            if (constructionManager.ConstructionJob.ArtisanCost == 0) artisanCostText.gameObject.SetActive(false);
            else artisanCostText.gameObject.SetActive(true);

            if (constructionManager.ConstructionJob.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (constructionManager.ConstructionJob.WoodCost == 0) woodCostText.gameObject.SetActive(false);
            else woodCostText.gameObject.SetActive(true);

            if (constructionManager.ConstructionJob.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (constructionManager.ConstructionJob.IronCost == 0) ironCostText.gameObject.SetActive(false);
            else ironCostText.gameObject.SetActive(true);

            projectText.text = constructionManager.ConstructionJob.Name;

            artisanCostText.text = $"Artisan Proficiency: {constructionManager.ConstructionJob.ArtisanCost}";
            SetColor(artisanCostText, guildhall.ArtisanProficiency, constructionManager.ConstructionJob.ArtisanCost);

            goldCostText.text = $"Gold: {guildhall.Gold} / {constructionManager.ConstructionJob.GoldCost}";
            SetColor(goldCostText, guildhall.Gold, constructionManager.ConstructionJob.GoldCost);

            woodCostText.text = $"Wood: {guildhall.Wood} / {constructionManager.ConstructionJob.WoodCost}";
            SetColor(woodCostText, guildhall.Wood, constructionManager.ConstructionJob.WoodCost);

            ironCostText.text = $"Iron: {guildhall.Iron} / {constructionManager.ConstructionJob.IronCost}";
            SetColor(ironCostText, guildhall.Iron, constructionManager.ConstructionJob.IronCost);

            timeText.text = Helpers.FormatTimer((int)constructionManager.ConstructionJob.constructionTime);
            expText.text = $"{constructionManager.ConstructionJob.Experience} Artisan Experience";
            upgradeText.text = constructionManager.ConstructionJob.Description;
        }
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(constructionManager.ConstructionJob);
        constructionManager.BeginConstruction();
    }

    public void PassUpgradeToSelectArtisans()
    {
        FindObjectOfType<Menu_SelectArtisans>().SetUpgrade(constructionManager.ConstructionJob);
    }

    private void SetColor(TextMeshProUGUI text, int have, int need)
    {
        if (have < need) text.color = Color.red;
        else text.color = Color.green;
    }

    private void SetButtons(Upgrade upgrade)
    {
        if (constructionManager.UnderConstruction || !constructionManager.ConstructionJob.CanAfford())
        {
            selectArtisansButton.interactable = false;
            buildButton.interactable = false;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
        }
        else
        {
            selectArtisansButton.interactable = true;
            buildButton.interactable = true;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
        }

        if (upgrade.ArtisanCost == 0)
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
