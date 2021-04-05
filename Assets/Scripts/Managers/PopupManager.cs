using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GenericPopup GenericPopup;
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

    public void SetPopupButtonText(string confirm)
    {
        popup.SetButtonText(confirm);
    }

    public void SetPopupButtonText(string confirm, string cancel)
    {
        popup.SetButtonText(confirm, cancel);
    }
}
