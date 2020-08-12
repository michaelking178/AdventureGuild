using UnityEngine;

public class QuestUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject questUI;

    private QuestManager questManager;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void UpdateQuestList()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.New))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
    }

    public void UpdateQuestJournalList()
    {
        questManager.SortQuestList(questManager.GetQuestPool());
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.Active))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
        foreach (Quest quest in questManager.GetQuestPool())
        {
            if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
            {
                GameObject newQuestUI = Instantiate(questUI, transform);
                newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
            }
        }
    }
}
