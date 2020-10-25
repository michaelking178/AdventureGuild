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
    private TextMeshProUGUI questDescription;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestMenu()
    {
        Quest quest = questManager.CurrentQuest;
        questName.text = quest.questName;
        questReward.text = Helpers.QuestRewardStr(quest);
        questDescription.text = quest.description;

        if (quest.QuestFaction != Quest.Faction.None)
        {
            questContractor.text = string.Format("{0} of the {1}", quest.contractor, quest.GetFactionString());
        }
        else
        {
            questContractor.text = quest.contractor;
        }
    }
}
