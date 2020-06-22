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
    private TextMeshProUGUI questDescription;

    [SerializeField]
    private TextMeshProUGUI time;

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        SetQuestUIAttributes();
    }

    private void SetQuestUIAttributes()
    {
        questName.text = quest.questName;
        questDescription.text = quest.description;

        time.text = (quest.time * 0.001).ToString() + " seconds";
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
}
