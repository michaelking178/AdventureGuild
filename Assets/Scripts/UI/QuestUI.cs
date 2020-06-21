using System.Collections;
using System.Collections.Generic;
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
        questName.text = quest.QuestName;
        questDescription.text = quest.Description;

        time.text = quest.TimeToComplete.ToString() + " seconds";
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
