using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    [SerializeField]
    private string itemName = "ITEM_NAME";

    [SerializeField]
    private string itemDescription = "ITEM_DESCRIPTION";

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image checkmarkImage;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private Color fontAvailableColor = new Color(0.196f, 0.196f, 0.196f, 1);
    private Color fontUnavailableColor = new Color(0.196f, 0.196f, 0.196f, 0.5f);

    private Sprite itemSprite;
    private Upgrade upgrade;
    private string defaultDescription;
    private bool isAvailable = true;
    private PopupManager popupManager;
    private MenuManager menuManager;

    private void Start()
    {
        defaultDescription = itemDescription;
        itemSprite = itemImage.sprite;
        itemNameText.text = itemName;
        popupManager = FindObjectOfType<PopupManager>();
        menuManager = FindObjectOfType<MenuManager>();
        upgrade = GetComponent<Upgrade>();
        StartCoroutine(DelayedCheckForPurchase());
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_UpgradeGuildhall")
        {
            CheckForPurchase();
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

    private IEnumerator DelayedCheckForPurchase()
    {
        yield return new WaitForSeconds(1.5f);
        CheckForPurchase();
        yield return null;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetAvailable()
    {
        isAvailable = true;
        itemImage.color = availableColor;
        itemNameText.color = fontAvailableColor;
    }

    public void SetUnavailable()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        itemNameText.color = fontUnavailableColor;
    }

    public void SetPurchased()
    {
        SetUnavailable();
        checkmarkImage.gameObject.SetActive(true);
    }

    public void CallPopup()
    {
        defaultDescription = itemDescription;
        if (upgrade.IsPurchased)
        {
            defaultDescription += "\n\n Purchased!";
            popupManager.CreateDefaultContent(defaultDescription);
        }
        else
        {
            popupManager.CreateUpgradeContent(defaultDescription, upgrade.GoldCost, upgrade.WoodCost, upgrade.IronCost);
        }

        if (isAvailable)
        {
            popupManager.SetDoubleButton("Purchase", "Cancel");
            popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(Confirm);
        }
        else
        {
            popupManager.SetSingleButton("Cancel");
        }
        popupManager.Populate(itemName, itemSprite, gameObject);
        popupManager.CallPopup();
    }

    private void Confirm()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(Confirm);
        GetComponent<Upgrade>().Apply();
    }

    public void SetItemAttributes(string _name, string _description)
    {
        itemName = _name;
        itemNameText.text = itemName;
        itemDescription = _description;
        defaultDescription = itemDescription;
    }
}
