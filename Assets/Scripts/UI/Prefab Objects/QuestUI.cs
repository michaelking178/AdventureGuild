using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{
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
    private QuestManager questManager;
    private MenuManager menuManager;
    private Menu_Quest menu_Quest;
    private Menu_QuestJournal questJournal;
    private Color emptySlotColor = new Color(0,0,0,0.25f);
    private Color combatColor = new Color(1,0,0,1);
    private Color espionageColor = new Color(1,1,0,1);
    private Color diplomacyColor = new Color(0,0,1,1);
    private Color factionColor = new Color(1,1,1,1);

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        menuManager = FindObjectOfType<MenuManager>();
        menu_Quest = menuManager.GetMenu("Menu_Quest").GetComponent<Menu_Quest>();
        questJournal = menuManager.GetMenu("Menu_QuestJournal").GetComponent<Menu_QuestJournal>();
    }

    private void FixedUpdate()
    {
        if (quest != null)
        {
            SetQuestUIAttributes();
        }
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        SetQuestUIAttributes();
        if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
        {
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            foreach (TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
    }

    private void SetQuestUIAttributes()
    {
        questName.text = quest.questName;
        questLevel.text = "Level " + quest.level;
        SetQuestUIState();
        SetSkillGem();
        SetFactionGem();
        SetRelicGem();
    }

    public void SetActiveQuest()
    {
        questManager.CurrentQuest = quest;
    }

    public void GoTo()
    {
        if (GetComponentInParent<QuestUIScrollView>().questUiMenu == "Menu_Quest")
        {
            menu_Quest.UpdateQuestMenu();
            menuManager.OpenMenu("Menu_Quest");
        }
        else if (GetComponentInParent<QuestUIScrollView>().questUiMenu == "Menu_QuestJournal")
        {
            questJournal.SetQuest(quest);
            questJournal.UpdateQuestJournal();
            menuManager.OpenMenu("Menu_QuestJournal");
        }
        else
        {
            Debug.Log("Cannot navigate from QuestUI to " + GetComponentInParent<QuestUIScrollView>().questUiMenu);
        }
    }

    private void SetQuestUIState()
    {
        if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
        {
            questTime.text = quest.State.ToString();
        }
        else if (quest.State == Quest.Status.New)
        {
            questTime.text = Helpers.FormatTimer(quest.time);
        }
        else if (quest.State == Quest.Status.Active)
        {
            foreach (GameObject child in Helpers.GetChildren(GameObject.Find("Quest Manager")))
            {
                if (child.GetComponent<QuestTimer>().GetQuest() == quest)
                {
                    float timeRemaining = child.GetComponent<QuestTimer>().TimeLimit - child.GetComponent<QuestTimer>().CurrentTime;
                    questTime.text = Helpers.FormatTimer((int)timeRemaining);
                }
            }
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
