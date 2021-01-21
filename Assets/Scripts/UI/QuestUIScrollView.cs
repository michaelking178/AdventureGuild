using System.Collections;
using UnityEngine;

public class QuestUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject questUI;

    public string questUiMenu = "";

    private QuestManager questManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestList()
    {
        questManager.SortQuestPoolByLevel();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.New))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            StartCoroutine(SetupQuestUI(quest, newQuestUI));
        }
    }

    public void UpdateQuestJournalList()
    {
        questManager.SortQuestPoolByStartTime();
        questManager.SortQuestArchiveByStartTime();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.Active))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
        foreach (Quest quest in questManager.GetQuestArchive())
        {
            if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
            {
                GameObject newQuestUI = Instantiate(questUI, transform);
                newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
            }
        }
    }

    private IEnumerator SetupQuestUI(Quest quest, GameObject questUI)
    {
        questUI.GetComponent<QuestUI>().SetQuest(quest);
        yield return new WaitForSeconds(0.5f);
        questUI.GetComponent<QuestUI>().ToggleExtensionPanel();
    }    
}
