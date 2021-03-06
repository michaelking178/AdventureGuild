﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIScrollView : MonoBehaviour
{
    private QuestManager questManager;
    private QuestUIPool questUIPool;
    private List<QuestUI> questUIs = new List<QuestUI>();

    private void Start() {
        questManager = FindObjectOfType<QuestManager>();
        questUIPool = FindObjectOfType<QuestUIPool>();
    }

    public void UpdateQuestList() {
        questManager.SortQuestPoolByLevel();
        ClearQuestUIs();

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.New))
            LoadQuestUI(quest);

        StartCoroutine(CloseAllExtensionPanels());
    }

    public void UpdateQuestJournalList() {
        questManager.SortQuestPoolByStartTime();
        questManager.SortQuestArchiveByStartTime();
        ClearQuestUIs();

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.Active))
        {
            LoadQuestUI(quest);
        }

        foreach (Quest quest in questManager.GetQuestArchive())
        {
            LoadQuestUI(quest);
        }

        foreach (QuestUI questUI in questUIs)
        {
            questUI.CloseExtensionPanel();
        }
    }

    private void LoadQuestUI(Quest quest) {
        QuestUI questUI = questUIPool.GetNextAvailableQuestUI();
        questUIs.Add(questUI);
        questUI.transform.SetParent(transform);
        questUI.gameObject.SetActive(true);
        questUI.SetQuest(quest);
    }

    public void ClearQuestUIs() {
        foreach (QuestUI questUI in questUIs)
        {
            questUI.ClearQuest();
        }
        questUIs.Clear();
    }

    private IEnumerator CloseAllExtensionPanels()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (QuestUI questUI in questUIs)
        {
            questUI.CloseExtensionPanel();
        }
    }
}
