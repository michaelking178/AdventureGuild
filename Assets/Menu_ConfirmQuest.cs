using UnityEngine;
using TMPro;

public class Menu_ConfirmQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI questAdventurer;

    [SerializeField]
    private TextMeshProUGUI questExperience;

    [SerializeField]
    private TextMeshProUGUI questReward;

    private QuestManager questManager;
    private Quest quest;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestMenu()
    {
        quest = questManager.GetCurrentQuest();
        questName.text = quest.questName;
        questAdventurer.text = QuestAdventurerStr(quest);
        questExperience.text = quest.Reward.Exp.ToString() + " Experience";
        questReward.text = Helpers.QuestRewardStr(quest);
    }

    private string QuestAdventurerStr(Quest quest)
    {
        return string.Format("{0} shall embark on this quest. Should {1} succeed, {1} will reap the following:", quest.GuildMember.person.name, Pronoun());
    }

    private string Pronoun()
    {
        if (quest.GuildMember.person.gender == "MALE")
        {
            return "he";
        }
        else
        {
            return "she";
        }
    }
}
