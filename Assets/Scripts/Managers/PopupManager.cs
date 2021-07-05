using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GenericPopup GenericPopup;
    public InfoPopup InfoPopup;
    public QuestPopup QuestPopup;

    public bool IsPopupOpen { get; private set; } = false;
    private List<PopupData> popupQueue = new List<PopupData>();
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (popupQueue.Count > 0 && !IsPopupOpen)
        {
            IsPopupOpen = true;
            if (popupQueue[0] is GenericPopupData genericPopupData)
            {
                CallGenericPopup(genericPopupData);
            }
            else if (popupQueue[0] is QuestPopupData questPopupData)
            {
                CallQuestPopup(questPopupData);
            }
            else if (popupQueue[0] is InfoPopupData infoPopupData)
            {
                CallInfoPopup(infoPopupData);
            }
            popupQueue.RemoveAt(0);
        }
    }

    /// <summary>
    /// Generic Popup is the basic informational popup with a "Confirm" and "Cancel" button
    /// </summary>
    /// <param name="title"></param>
    /// <param name="subtitle"></param>
    /// <param name="description"></param>
    /// <param name="sprite"></param>
    public void RequestGenericPopup(string title, string subtitle, string description, Sprite sprite)
    {
        PopupData newPopup = new GenericPopupData(title, subtitle, description, sprite);
        popupQueue.Add(newPopup);
    }

    /// <summary>
    /// Generic Popup overload that includes customizable button text
    /// </summary>
    /// <param name="title"></param>
    /// <param name="subtitle"></param>
    /// <param name="description"></param>
    /// <param name="sprite"></param>
    /// <param name="confirmText"></param>
    /// <param name="cancelText"></param>
    public void RequestGenericPopup(string title, string subtitle, string description, Sprite sprite, string confirmText, string cancelText)
    {
        PopupData newPopup = new GenericPopupData(title, subtitle, description, sprite, confirmText, cancelText);
        popupQueue.Add(newPopup);
    }

    /// <summary>
    /// Quest Popup only appears when viewing a quest on the Questboard
    /// </summary>
    /// <param name="quest"></param>
    public void RequestQuestPopup(Quest quest)
    {
        PopupData newPopup = new QuestPopupData(quest);
        popupQueue.Add(newPopup);
    }

    /// <summary>
    /// Info Popup is the basic informational popup with only a "Confirm" button
    /// </summary>
    /// <param name="title"></param>
    /// <param name="subtitle"></param>
    /// <param name="description"></param>
    /// <param name="sprite"></param>
    public void RequestInfoPopup(string title, string subtitle, string description, Sprite sprite)
    {
        PopupData newPopup = new InfoPopupData(title, subtitle, description, sprite);
        popupQueue.Add(newPopup);
    }

    /// <summary>
    /// Info Popup overload that includes customizable Confirm button text
    /// </summary>
    /// <param name="title"></param>
    /// <param name="subtitle"></param>
    /// <param name="description"></param>
    /// <param name="sprite"></param>
    /// <param name="confirmText"></param>
    /// <param name="cancelText"></param>
    public void RequestInfoPopup(string title, string subtitle, string description, Sprite sprite, string confirmText, string cancelText)
    {
        PopupData newPopup = new InfoPopupData(title, subtitle, description, sprite, confirmText);
        popupQueue.Add(newPopup);
    }

    public void SetIsOpen(bool _isOpen)
    {
        IsPopupOpen = _isOpen;
    }

    private void CallQuestPopup(QuestPopupData popupData)
    {
        QuestPopup.Populate(popupData);
    }

    private void CallGenericPopup(GenericPopupData popupData)
    {
        GenericPopup.Populate(popupData);
    }

    private void CallInfoPopup(InfoPopupData popupData)
    {
        InfoPopup.Populate(popupData);
    }
}
