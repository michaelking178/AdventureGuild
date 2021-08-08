using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    public string UpgradeName;

    [SerializeField]
    private TierUpgrade upgrade;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image tintPanel;

    [SerializeField]
    private Image checkmarkImage;

    [SerializeField]
    private GameObject timerPanel;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private TierUpgradeObject upgradeTier;
    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private MenuManager menuManager;
    private ConstructionManager constructionManager;
    private Button panelBtn;
    private bool isAvailable = true;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        constructionManager = FindObjectOfType<ConstructionManager>();
        upgrade = constructionManager.GetUpgrade(UpgradeName);
        panelBtn = GetComponent<Button>();
        StartCoroutine(DelayedCheckForPurchase());
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_UpgradeGuildhall" && upgrade != null)
        {
            SetUpgradeTier();
            CheckForPurchase();
            itemNameText.text = upgradeTier.Name;

            if (descriptionText != null)
                descriptionText.text = upgradeTier.Description;

            if (constructionManager.ConstructionJob != null && constructionManager.ConstructionJob.name == upgrade.name && constructionManager.UnderConstruction)
            {
                DisplayTimer();
                panelBtn.interactable = false;
            }
            else
            {
                HideTimer();
            }
        }
    }

    public void CheckForPurchase()
    {
        if (upgrade != null)
        {
            if (upgrade.IsPurchased)
                SetPurchased();
            else if (upgrade.CanAfford())
                SetAvailable();
            else
                SetUnavailable();
        }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetAvailable()
    {
        isAvailable = true;
        itemImage.color = availableColor;
        panelBtn.interactable = true;
    }

    public void SetUnavailable()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        panelBtn.interactable = true;
    }

    public void SetPurchased()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        checkmarkImage.gameObject.SetActive(true);
        tintPanel.gameObject.SetActive(true);
        panelBtn.interactable = false;
    }

    public void AssignConstructionJob()
    {
        if (!constructionManager.UnderConstruction)
            constructionManager.SetConstructionJob(upgrade);

        FindObjectOfType<Menu_Construction>().ConstructionJob = upgrade;
        FindObjectOfType<Menu_Construction>().Open();
    }

    public void DisplayTimer()
    {
        timerPanel.SetActive(true);
        timerText.text = Helpers.FormatTimer((int)(upgradeTier.constructionTime - constructionManager.TimeElapsed));
    }

    public void HideTimer()
    {
        timerPanel.SetActive(false);
        timerText.text = "";
    }

    private IEnumerator DelayedCheckForPurchase()
    {
        yield return new WaitForSeconds(1.5f);
        CheckForPurchase();
        yield return null;
    }

    private void SetUpgradeTier()
    {
        upgradeTier = upgrade.UpgradeTiers[upgrade.NextTier()];
    }
}
