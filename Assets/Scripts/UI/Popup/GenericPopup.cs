using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericPopup : PopupMenu
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI subtitle;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Image image;

    #endregion

    public void Populate(GenericPopupData popupData)
    {
        title.text = popupData.Title;
        subtitle.text = popupData.Subtitle;
        description.text = popupData.Description;
        image.gameObject.SetActive(true);
        image.sprite = popupData.Sprite;
        SetButtonText(popupData.ConfirmText, popupData.CancelText);
        base.Populate();
    }

    private void SetButtonText(string confirm, string cancel)
    {
        ConfirmBtn.GetComponentInChildren<TextMeshProUGUI>().text = confirm;
        CancelBtn.GetComponentInChildren<TextMeshProUGUI>().text = cancel;
    }
}
