using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TextMeshProUGUI itemName;

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private Color fontAvailableColor = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
    private Color fontUnavailableColor = new Color(0.1960784f, 0.1960784f, 0.1960784f, 0.5f);
    private bool isAvailable = false;

    #region debug stuff
    [SerializeField]
    private bool ToggleAvailability = true;

    private void FixedUpdate()
    {
        if (ToggleAvailability == true && isAvailable == false)
        {
            SetAvailable();
        }
        else if (ToggleAvailability == false && isAvailable == true)
        {
            SetUnavailable();
        }
    }
    #endregion

    public void SetAvailable()
    {
        isAvailable = true;
        itemImage.color = availableColor;
        itemName.color = fontAvailableColor;
    }

    public void SetUnavailable()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        itemName.color = fontUnavailableColor;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }
}
