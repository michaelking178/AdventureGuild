using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    public string UpgradeName;

    [SerializeField]
    private Upgrade upgrade;

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

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private Color purchasedColor = new Color(1, 1, 1, 0.25f);
    private Color fontAvailableColor = new Color(0.196f, 0.196f, 0.196f, 1);
    private Color fontUnavailableColor = new Color(0.196f, 0.196f, 0.196f, 0.5f);
    private MenuManager menuManager;
    private ConstructionManager constructionManager;
    private bool isAvailable = true;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        constructionManager = FindObjectOfType<ConstructionManager>();
        upgrade = constructionManager.GetUpgrade(UpgradeName);
        itemNameText.text = upgrade.Name;
        StartCoroutine(DelayedCheckForPurchase());
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_UpgradeGuildhall" && upgrade != null)
        {
            CheckForPurchase();
            itemNameText.text = upgrade.Name;

            if (descriptionText != null)
                descriptionText.text = upgrade.Description;

            if (constructionManager.ConstructionJob != null && constructionManager.ConstructionJob.name == upgrade.name && constructionManager.UnderConstruction)
            {
                DisplayTimer();
            }
            else
            {
                HideTimer();
            }
        }
    }

    public void CheckForPurchase()
    {
        if (upgrade != null && !upgrade.IsPurchased && upgrade.CanAfford())
        {
            SetAvailable();
        }
        else if (upgrade != null && upgrade.IsPurchased)
        {
            SetPurchased();
        }
        else
        {
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
        //itemNameText.color = fontAvailableColor;
    }

    public void SetUnavailable()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        //itemNameText.color = fontUnavailableColor;
    }

    public void SetPurchased()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        //itemNameText.color = fontUnavailableColor;
        checkmarkImage.gameObject.SetActive(true);
        tintPanel.gameObject.SetActive(true);
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
        timerText.text = Helpers.FormatTimer((int)(upgrade.constructionTime - constructionManager.TimeElapsed));
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
}
