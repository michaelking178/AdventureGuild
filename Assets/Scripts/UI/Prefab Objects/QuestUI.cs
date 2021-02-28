using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Extension Panel")]
    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private TextMeshProUGUI contractorText;

    [SerializeField]
    private TextMeshProUGUI rewardText;

    [SerializeField]
    private TextMeshProUGUI briefingText;

    private Quest quest;
    private QuestManager questManager;
    private MenuManager menuManager;
    private Menu_QuestJournal questJournal;
    private QuestUIPool questUIPool;
    private Color emptySlotColor = new Color(0,0,0,0.25f);
    private Color combatColor = new Color(1,0,0,1);
    private Color espionageColor = new Color(1,1,0,1);
    private Color diplomacyColor = new Color(0,0,1,1);
    private Color factionColor = new Color(1,1,1,1);

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        menuManager = FindObjectOfType<MenuManager>();
        questUIPool = FindObjectOfType<QuestUIPool>();
        questJournal = FindObjectOfType<Menu_QuestJournal>();
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
        SetSkillGem();
        SetFactionGem();
        SetRelicGem();
        if (extensionPanel != null)
        {
            SetExtensionPanelContent();
            ToggleExtensionPanel();
        }
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

    public void ClearQuest()
    {
        quest = null;
        transform.SetParent(questUIPool.transform);
        gameObject.SetActive(false);
    }

    public void SetActiveQuest()
    {
        questManager.CurrentQuest = quest;
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

    private void SetExtensionPanelContent()
    {
        if (quest.State == Quest.Status.New)
        {
            if (quest.faction != "")
            {
                contractorText.text = quest.contractor + " of the " + quest.GetFactionString();
            }
            else
            {
                contractorText.text = quest.contractor;
            }
            rewardText.text = Helpers.QuestRewardStr(quest);
            briefingText.text = quest.description;
            foreach (TextSizer textSizer in GetComponentsInChildren<TextSizer>())
            {
                textSizer.Refresh();
            }
        }
    }

    public void ToggleExtensionPanel()
    {
        if (quest.State == Quest.Status.New)
        {
            if (extensionPanel.activeInHierarchy)
            {
                extensionPanel.SetActive(false);
            }
            else
            {
                extensionPanel.SetActive(true);
            }
        }
        else
        {
            extensionPanel.SetActive(false);
        }
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
    }

    public void AcceptQuest()
    {
        Menu_SelectAdventurer menu = FindObjectOfType<Menu_SelectAdventurer>();
        SetActiveQuest();
        menu.GetComponentInChildren<PersonUIScrollView>().GetAvailableAdventurersUI();
        menuManager.OpenMenu(menu);
    }

    public void HandleButtonClick()
    {
        if (quest.State == Quest.Status.New)
        {
            ToggleExtensionPanel();
        }
        else
        {
            questJournal.SetQuest(quest);
            questJournal.UpdateQuestJournal();
            menuManager.OpenMenu(FindObjectOfType<Menu_QuestJournal>());
        }
    }
}
