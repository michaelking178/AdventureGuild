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
    private int goldCost;

    [SerializeField]
    private int ironCost;

    [SerializeField]
    private int woodCost;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private Color fontAvailableColor = new Color(0.196f, 0.196f, 0.196f, 1);
    private Color fontUnavailableColor = new Color(0.196f, 0.196f, 0.196f, 0.5f);
    private string defaultDescription;
    private bool isAvailable = true;
    private Sprite itemSprite;
    private Guildhall guildhall;

    private void Start()
    {
        defaultDescription = itemDescription;
        itemSprite = itemImage.sprite;
        itemNameText.text = itemName;
        guildhall = FindObjectOfType<Guildhall>();
    }

    private void FixedUpdate()
    {
        if (CanAfford())
        {
            SetAvailable();
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
        if (!isAvailable)
        {
            isAvailable = true;
            itemImage.color = availableColor;
            itemNameText.color = fontAvailableColor;
        }
    }

    public void SetUnavailable()
    {
        if (isAvailable)
        {
            isAvailable = false;
            itemImage.color = unavailableColor;
            itemNameText.color = fontUnavailableColor;
        }
    }

    public void CallPopup()
    {
        popup.gameObject.SetActive(true);
        defaultDescription = itemDescription;
        defaultDescription += "\nYOU HAVE: Gold: " + guildhall.Gold.ToString() + ", Wood: " + guildhall.Wood.ToString() + ", Iron: " + guildhall.Iron.ToString();
        defaultDescription += "\nCOST: Gold: " + goldCost.ToString() + ", Wood: " + woodCost.ToString() + ", Iron: " + ironCost.ToString();

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
        GetComponent<IUpgrade>().Apply();
    }

    private bool CanAfford()
    {
        if (guildhall.Gold < goldCost || (guildhall.Wood < woodCost) || (guildhall.Iron < ironCost))
        {
            return false;
        }
        return true;
    }
}
