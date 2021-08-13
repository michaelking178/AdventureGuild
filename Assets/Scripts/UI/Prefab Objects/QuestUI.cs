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

    private Quest quest;
    private QuestUIPool questUIPool;

    private Color questCompletedColor = new Color(0.4f, 0.4f, 0.4f);
    private Image questUIPanel;

    #endregion

    private void Start()
    {
        questUIPool = FindObjectOfType<QuestUIPool>();
        questUIPanel = GetComponentInChildren<Image>();
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
        {
            questTime.text = quest.State.ToString();
            questUIPanel.color = questCompletedColor;
            foreach(TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = questCompletedColor;
            }
        }
        else if (quest.State == Quest.Status.New)
        {
            questTime.text = Helpers.FormatTimer(quest.time);
            questUIPanel.color = Color.white;
            foreach (TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = Color.white;
            }
        }
        else if (quest.State == Quest.Status.Active)
        {
            float timeRemaining = quest.Timer.TimeLimit - quest.Timer.CurrentTime;
            questTime.text = Helpers.FormatTimer((int)timeRemaining);
            questUIPanel.color = Color.white;
            foreach (TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = Color.white;
            }
        }
    }
}
