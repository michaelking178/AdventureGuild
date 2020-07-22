using UnityEngine;
using TMPro;

public class Menu_Quest : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI questDescription;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestMenu()
    {
        Quest quest = questManager.GetCurrentQuest();
        questName.text = quest.questName;
        questDescription.text = quest.description;
    }
}
