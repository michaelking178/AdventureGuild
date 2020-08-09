using UnityEngine;
using TMPro;

public class Menu_Quest : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI questContractor;

    [SerializeField]
    private TextMeshProUGUI questReward;

    [SerializeField]
    private TextMeshProUGUI questExperience;

    [SerializeField]
    private TextMeshProUGUI questDescription;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestMenu()
    {
        Quest quest = questManager.CurrentQuest;
        questName.text = quest.questName;
        questContractor.text = quest.contractor;
        questReward.text = Helpers.QuestRewardStr(quest);
        questDescription.text = quest.description;
    }
}
