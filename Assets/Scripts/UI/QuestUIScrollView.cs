using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject questUI;

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

        StartCoroutine(ToggleAllExtensionPanels());
    }

    public void UpdateQuestJournalList() {
        questManager.SortQuestPoolByStartTime();
        questManager.SortQuestArchiveByStartTime();
        ClearQuestUIs();

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.Active))
            LoadQuestUI(quest);

        foreach (Quest quest in questManager.GetQuestArchive())
        {
            if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
                LoadQuestUI(quest);
        }
    }

    private void LoadQuestUI(Quest quest) {
        QuestUI questUI = questUIPool.GetNextAvailableQuestUI();
        questUI.gameObject.SetActive(true);
        questUIs.Add(questUI);
        questUI.SetQuest(quest);
        questUI.transform.SetParent(transform);
    }

    private void ClearQuestUIs() {
        foreach (QuestUI questUI in questUIs)
        {
            questUI.ClearQuest();
        }
        questUIs.Clear();
    }

    private IEnumerator ToggleAllExtensionPanels()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (QuestUI questUI in questUIs)
        {
            questUI.ToggleExtensionPanel();
        }
    }
}
