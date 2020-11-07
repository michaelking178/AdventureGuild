using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject skillGem;

    [SerializeField]
    private GameObject factionGem;

    [SerializeField]
    private GameObject relicGem;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestMenu()
    {
        Quest quest = questManager.CurrentQuest;

        SetSkillGem(quest);
        SetFactionGem(quest);
        SetRelicGem(quest);
        
        questName.text = quest.questName;
        questReward.text = Helpers.QuestRewardStr(quest);
        questDescription.text = quest.description;

        if (quest.QuestFaction != Quest.Faction.None)
        {
            questContractor.text = $"{quest.contractor} of the {quest.GetFactionString()}";
        }
        else
        {
            questContractor.text = quest.contractor;
        }
    }

    private void SetSkillGem(Quest quest)
    {
        if (quest.QuestSkill != Quest.Skill.None)
        {
            skillGem.GetComponentInChildren<TextMeshProUGUI>().text = quest.skill;

            switch (quest.QuestSkill)
            {
                case Quest.Skill.Combat:
                    skillGem.GetComponentInChildren<Image>().color = Color.red;
                    break;
                case Quest.Skill.Espionage:
                    skillGem.GetComponentInChildren<Image>().color = Color.yellow;
                    break;
                case Quest.Skill.Diplomacy:
                    skillGem.GetComponentInChildren<Image>().color = Color.blue;
                    break;
                default:
                    break;
            }
        }
        else
        {
            skillGem.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.25f);
            skillGem.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    private void SetFactionGem(Quest quest)
    {
        if (quest.QuestFaction != Quest.Faction.None)
        {
            factionGem.GetComponentInChildren<Image>().color = Color.white;
            factionGem.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetFactionString();
        }
        else
        {
            factionGem.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.25f);
            factionGem.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    private void SetRelicGem(Quest quest)
    {
        relicGem.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.25f);
        relicGem.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
