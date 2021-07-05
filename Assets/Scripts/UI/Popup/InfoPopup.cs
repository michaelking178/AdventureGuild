using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPopup : PopupMenu
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI subtitle;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Image image;

    #endregion

    public void Populate(InfoPopupData popupData)
    {
        title.text = popupData.Title;
        subtitle.text = popupData.Subtitle;
        description.text = popupData.Description;
        image.gameObject.SetActive(true);
        image.sprite = popupData.Sprite;
        SetButtonText(popupData.ConfirmText);
        base.Populate();
    }

    protected void SetButtonText(string confirm)
    {
        ConfirmBtn.GetComponentInChildren<TextMeshProUGUI>().text = confirm;
    }
}
