using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    private Quest quest;

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

    private QuestManager questManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
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

        questReward.text = Helpers.QuestRewardStr(quest);
        questTime.text = (quest.time * 0.001).ToString() + " seconds";
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

    public void UpdateQuestMenu()
    {
        GameObject.Find("Menu_Quest").GetComponent<Menu_Quest>().UpdateQuestMenu();
    }

    public void GoToQuestMenu()
    {
        FindObjectOfType<MenuManager>().OpenMenu("Menu_Quest");
    }

    public void UpdateQuestJournal()
    {
        GameObject.Find("Menu_QuestJouranl").GetComponent<Menu_QuestJournal>().UpdateQuestJournal();
    }

    public void GoToQuestJournal()
    {
        FindObjectOfType<Menu_QuestJournal>().SetQuest(quest);
        FindObjectOfType<Menu_QuestJournal>().UpdateQuestJournal();
        FindObjectOfType<MenuManager>().OpenMenu("Menu_QuestJournal");
    }
}
