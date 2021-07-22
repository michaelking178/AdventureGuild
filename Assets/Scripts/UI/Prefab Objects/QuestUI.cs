using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI questTime;

    [SerializeField]
    private TextMeshProUGUI questLevel;

    [SerializeField]
    private Image skillIcon;
    
    [SerializeField]
    private Image factionIcon;

    [SerializeField]
    private Image relicIcon;

    private Quest quest;
    private QuestUIPool questUIPool;
    private Color emptySlotColor = new Color(0,0,0,0.25f);
    private Color combatColor = new Color(1,0,0,1);
    private Color espionageColor = new Color(1,1,0,1);
    private Color diplomacyColor = new Color(0,0,1,1);
    private Color factionColor = new Color(1,1,1,1);

    #endregion

    private void Start()
    {
        questUIPool = FindObjectOfType<QuestUIPool>();
    }

    private void FixedUpdate()
    {
        if (quest != null)
        {
            SetQuestUIState();
        }
    }

    public Quest GetQuest()
    {
        return quest;
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        questName.text = quest.questName;
        questLevel.text = "Level " + quest.level;
        //SetSkillGem();
        //SetFactionGem();
        //SetRelicGem();
    }

    public void ClearQuest()
    {
        quest = null;
        transform.SetParent(questUIPool.transform);
        gameObject.SetActive(false);
    }

    public void AcceptQuest()
    {
        FindObjectOfType<QuestManager>().CurrentQuest = quest;
        FindObjectOfType<Menu_SelectAdventurer>().Open();
    }

    public void HandleButtonClick()
    {
        FindObjectOfType<Menu_QuestJournal>().SetQuest(quest);
        FindObjectOfType<Menu_QuestJournal>().Open();
    }

    private void SetQuestUIState()
    {
        if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
            questTime.text = quest.State.ToString();
        else if (quest.State == Quest.Status.New)
            questTime.text = Helpers.FormatTimer(quest.time);
        else if (quest.State == Quest.Status.Active)
        {
            float timeRemaining = quest.Timer.TimeLimit - quest.Timer.CurrentTime;
            questTime.text = Helpers.FormatTimer((int)timeRemaining);
        }
    }

    private void SetSkillGem()
    {
        if (skillIcon != null)
        {
            switch (quest.QuestSkill)
            {
                case Quest.Skill.Combat:
                    skillIcon.color = combatColor;
                    break;
                case Quest.Skill.Espionage:
                    skillIcon.color = espionageColor;
                    break;
                case Quest.Skill.Diplomacy:
                    skillIcon.color = diplomacyColor;
                    break;
                default:
                    skillIcon.color = emptySlotColor;
                    break;
            }
        }
    }

    private void SetFactionGem()
    {
        if (factionIcon != null)
        {
            switch (quest.QuestFaction)
            {
                case Quest.Faction.MagesGuild:
                    factionIcon.color = factionColor;
                    break;
                case Quest.Faction.MerchantsGuild:
                    factionIcon.color = factionColor;
                    break;
                case Quest.Faction.RoyalPalace:
                    factionIcon.color = factionColor;
                    break;
                default:
                    factionIcon.color = emptySlotColor;
                    break;
            }
        }
    }

    private void SetRelicGem()
    {
        relicIcon.color = emptySlotColor;
    }
}
