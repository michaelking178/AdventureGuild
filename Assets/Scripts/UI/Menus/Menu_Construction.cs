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
    private Upgrade upgrade;

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
            if (constructionManager.ConstructionJob != null && (constructionManager.UnderConstruction || !constructionManager.ConstructionJob.CanAfford()))
            {
                buildButton.interactable = false;
                buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            }
            else
            {
                buildButton.interactable = true;
                buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
            }
        }
    }

    public void SetUpgrade(Upgrade _upgrade)
    {
        upgrade = _upgrade;
    }

    public void Populate()
    {
        SetButtons(upgrade);

        if (upgrade.ArtisanCost == 0) artisanCostText.gameObject.SetActive(false);
        else artisanCostText.gameObject.SetActive(true);

        if (upgrade.GoldCost == 0) goldCostText.gameObject.SetActive(false);
        else goldCostText.gameObject.SetActive(true);

        if (upgrade.WoodCost == 0) woodCostText.gameObject.SetActive(false);
        else woodCostText.gameObject.SetActive(true);

        if (upgrade.GoldCost == 0) goldCostText.gameObject.SetActive(false);
        else goldCostText.gameObject.SetActive(true);

        if (upgrade.IronCost == 0) ironCostText.gameObject.SetActive(false);
        else ironCostText.gameObject.SetActive(true);

        projectText.text = upgrade.Name;

        artisanCostText.text = $"Artisan Proficiency: {upgrade.ArtisanCost}";
        SetColor(artisanCostText, guildhall.ArtisanProficiency, upgrade.ArtisanCost);

        goldCostText.text = $"Gold: {guildhall.Gold} / {upgrade.GoldCost}";
        SetColor(goldCostText, guildhall.Gold, upgrade.GoldCost);

        woodCostText.text = $"Wood: {guildhall.Wood} / {upgrade.WoodCost}";
        SetColor(woodCostText, guildhall.Wood, upgrade.WoodCost);

        ironCostText.text = $"Iron: {guildhall.Iron} / {upgrade.IronCost}";
        SetColor(ironCostText, guildhall.Iron, upgrade.IronCost);

        timeText.text = Helpers.FormatTimer((int)upgrade.constructionTime);
        expText.text = $"{upgrade.Experience} Artisan Experience";
        upgradeText.text = upgrade.Description;
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(upgrade);
        constructionManager.BeginConstruction();
    }

    public void PassUpgradeToSelectArtisans()
    {
        FindObjectOfType<Menu_SelectArtisans>().SetUpgrade(upgrade);
    }

    private void SetColor(TextMeshProUGUI text, int have, int need)
    {
        if (have < need) text.color = Color.red;
        else text.color = Color.green;
    }

    private void SetButtons(Upgrade upgrade)
    {
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
