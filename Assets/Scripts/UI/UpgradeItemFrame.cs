using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    [SerializeField]
    private PopupMenu popup;

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
    private Guildhall guildhall;
    private Upgrade upgrade;
    private string defaultDescription;
    private bool isAvailable = true;

    private void Start()
    {
        defaultDescription = itemDescription;
        itemSprite = itemImage.sprite;
        itemNameText.text = itemName;
        guildhall = FindObjectOfType<Guildhall>();
        upgrade = GetComponent<Upgrade>();
        StartCoroutine(CheckForPurchase());
    }

    private IEnumerator CheckForPurchase()
    {
        yield return new WaitForSeconds(1.5f);
        if (upgrade != null && !upgrade.IsPurchased && upgrade.CanAfford())
        {
            SetAvailable();
        }
        else if (upgrade != null && upgrade.IsPurchased)
        {
            SetUnavailable();
            SetPurchased();
        }
        else
        {
            SetUnavailable();
        }
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
        checkmarkImage.gameObject.SetActive(true);
    }

    public void CallPopup()
    {
        popup.gameObject.SetActive(true);
        defaultDescription = itemDescription;

        if (upgrade.IsPurchased)
        {
            defaultDescription += "\n Purchased!";
        }
        else
        {
            defaultDescription += "\nYOU HAVE: Gold: " + guildhall.Gold.ToString() + ", Wood: " + guildhall.Wood.ToString() + ", Iron: " + guildhall.Iron.ToString();
            defaultDescription += "\nCOST: Gold: " + upgrade.GoldCost.ToString() + ", Wood: " + upgrade.WoodCost.ToString() + ", Iron: " + upgrade.IronCost.ToString();
        }

        if (isAvailable)
        {
            popup.SetDoubleButton("Purchase", "Cancel");
            popup.GetComponentInChildren<Button>().onClick.AddListener(Confirm);
        }
        else
        {
            popup.SetSingleButton("Cancel");
        }
        popup.Populate(itemName, defaultDescription, itemSprite, gameObject);
    }

    private void Confirm()
    {
        popup.GetComponentInChildren<Button>().onClick.RemoveListener(Confirm);
        GetComponent<Upgrade>().Apply();
        SetPurchased();
    }
}
