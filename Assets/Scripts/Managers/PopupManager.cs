using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GenericPopup GenericPopup;
    public InfoPopup InfoPopup;
    public QuestPopup QuestPopup;

    private PopupMenu popup;

    private List<PopupData> popupQueue = new List<PopupData>();

    public void CallQuestPopup(Quest quest)
    {
        popup = QuestPopup;
        QuestPopup.Populate(quest);
        QuestPopup.SetButtonText("Accept", "Cancel");
    }

    /// <summary>
    /// Call a Generic Popup (if no subtitle is needed, use "")
    /// </summary>
    /// <param name="_title"></param>
    /// <param name="_subtitle"></param>
    /// <param name="_description"></param>
    /// <param name="_sprite"></param>
    public void CallGenericPopup(string _title, string _subtitle, string _description, Sprite _sprite)
    {
        popup = GenericPopup;
        GenericPopup.Populate(_title, _subtitle, _description, _sprite);
        GenericPopup.SetButtonText("Confirm", "Cancel");
    }

    /// <summary>
    /// Call an Info Popup including a subtitle (use "" to exclude the subtitle)
    /// </summary>
    /// <param name="_title"></param>
    /// <param name="_subtitle"></param>
    /// <param name="_description"></param>
    /// <param name="_sprite"></param>
    public void CallInfoPopup(string _title, string _subtitle, string _description, Sprite _sprite)
    {
        popup = InfoPopup;
        InfoPopup.Populate(_title, _subtitle, _description, _sprite);
        InfoPopup.SetButtonText("Okay");
    }

    public void SetPopupButtonText(string confirm)
    {
        popup.SetButtonText(confirm);
    }

    public void SetPopupButtonText(string confirm, string cancel)
    {
        popup.SetButtonText(confirm, cancel);
    }

    public bool IsGenericPopupOpen()
    {
        return GenericPopup.IsOpen;
    }

    public void CreatePopup(PopupData.PopupType popupType, string title, string subtitle, string description, Sprite sprite)
    {
        PopupData newPopup = new PopupData(popupType, title, subtitle, description, sprite);
        popupQueue.Add(newPopup);
    }
}
