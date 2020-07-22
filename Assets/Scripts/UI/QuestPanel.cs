using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField]
    private TextMeshProUGUI activeQuests;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestPanel()
    {
        activeQuests.text = questManager.GetAvailableQuests().Count.ToString();
    }
}
