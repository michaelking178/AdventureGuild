using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI questTime;

    [SerializeField]
    private TextMeshProUGUI questDifficulty;

    [SerializeField]
    private TextMeshProUGUI questExperience;

    [SerializeField]
    private TextMeshProUGUI questReward;

    private Quest quest;
    private QuestManager questManager;
    private MenuManager menuManager;
    private Menu_Quest menu_Quest;
    private Menu_QuestJournal questJournal;

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
            SetQuestUIAttributes();
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        SetQuestUIAttributes();
    }

    private void SetQuestUIAttributes()
    {
        questName.text = quest.questName;
        questExperience.text = quest.Reward.Exp.ToString();

        if (quest.difficulty == 0)
        {
            questDifficulty.text = "Easy";
        }
        else if (quest.difficulty == 1)
        {
            questDifficulty.text = "Medium";
        }
        else if (quest.difficulty == 2)
        {
            questDifficulty.text = "Hard";
        }
        else if (quest.difficulty == 3)
        {
            questDifficulty.text = "Very Hard";
        }

        questReward.text = Helpers.QuestRewardStr(quest);
        if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
        {
            questTime.text = quest.State.ToString();
        }
        else if (quest.State == Quest.Status.New)
        {
            questTime.text = quest.time.ToString() + " seconds";
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

    public void ShowPanel()
    {
        if (extensionPanel.activeSelf)
        {
            extensionPanel.SetActive(false);
        }
        else
        {
            extensionPanel.SetActive(true);
        }
    }

    public void SetActiveQuest()
    {
        questManager.CurrentQuest = quest;
    }

    public void GoToQuestMenu()
    {
        menu_Quest.UpdateQuestMenu();
        menuManager.OpenMenu("Menu_Quest");
    }

    public void GoToQuestJournal()
    {
        questJournal.SetQuest(quest);
        questJournal.UpdateQuestJournal();
        menuManager.OpenMenu("Menu_QuestJournal");
    }
}
