using TMPro;
using UnityEngine;

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
        questExperience.text = quest.reward.Exp.ToString();

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

        string reward = "";
        if (quest.reward.Gold != 0)
        {
            reward += quest.reward.Gold.ToString() + " Gold";
        }
        if (quest.reward.Wood != 0)
        {
            reward += ", " + quest.reward.Wood.ToString() + " Wood";
        }
        if (quest.reward.Iron != 0)
        {
            reward += ", " + quest.reward.Iron.ToString() + " Iron";
        }
        questReward.text = reward;

        questTime.text = (quest.time * 0.001).ToString() + " seconds";
    }

    public void ShowPanel()
    {
        Debug.Log(name + " Clicked");
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
        questManager.SetActiveQuest(quest);
    }

    public void UpdateQuestMenu()
    {
        FindObjectOfType<Menu_Quest>().UpdateQuestMenu();
    }

    public void GoToQuestMenu()
    {
        FindObjectOfType<MenuManager>().OpenMenu("Menu_Quest");
    }
}
