using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestPost : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI questName, questLevel, questTime;

    private Menu_Questboard questBoard;
    private Quest quest;

    private void Start()
    {
        questBoard = FindObjectOfType<Menu_Questboard>();
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        questName.text = quest.questName;
        questLevel.text = $"Level {quest.level}";
        questTime.text = Helpers.FormatTimer(quest.time);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Selecting quest: {quest.questName}");
        CallPopup();
    }

    private void CallPopup()
    {
        GetComponent<ButtonFunctions>().PlaySound();
        PopupManager popupManager = FindObjectOfType<PopupManager>();
        popupManager.CallQuestPopup(quest);
        popupManager.SetPopupButtonText("Accept", "Cancel");
        popupManager.QuestPopup.ConfirmBtn.onClick.AddListener(StartQuest);
    }

    private void StartQuest()
    {
        FindObjectOfType<QuestManager>().CurrentQuest = quest;
        FindObjectOfType<Menu_SelectAdventurer>().Open();
        Debug.Log($"Choosing an adventurer for {quest.questName}...");
    }
}
