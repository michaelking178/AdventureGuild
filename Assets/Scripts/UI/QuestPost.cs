using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPost : MonoBehaviour
{
    public TextMeshProUGUI questName, questLevel, questTime;

    [SerializeField]
    private Image levelTint;

    private Quest quest;
    private int maxQuestLevel= 20;
    private float questFraction;

    private void Awake()
    {
        questFraction = 1.0f / (maxQuestLevel / 2);
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        questName.text = quest.questName;
        questLevel.text = $"Level {quest.level}";
        questTime.text = Helpers.FormatTimer(quest.time);
        SetColor();
    }

    public void CallPopup()
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
    }

    private void SetColor()
    {
        float r = 0.0f;
        float g = 1.0f;

        for (int i = 0; i < quest.level; i++)
        {
            
            if (r == 1.0f)
            {
                g -= questFraction;
            }
            else
            {
                r += questFraction;
            }
        }
        levelTint.color = new Color(r, g, 0, 0.5f);
    }
}
