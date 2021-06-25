using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GenericPopup GenericPopup;
    public InfoPopup InfoPopup;
    public QuestPopup QuestPopup;

    private PopupMenu popup;

    public void CallQuestPopup(Quest quest)
    {
        popup = QuestPopup;
        QuestPopup.Populate(quest);
        QuestPopup.SetButtonText("Accept", "Cancel");
    }

    public void CallGenericPopup(string _title, string _description, Sprite _sprite)
    {
        popup = GenericPopup;
        GenericPopup.Populate(_title, _description, _sprite);
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
}
