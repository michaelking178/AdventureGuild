using UnityEngine;

public class QuestUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject questUI;

    public void UpdateQuestList()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (Quest quest in FindObjectOfType<QuestManager>().GetQuestsByStatus(Quest.Status.New))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
    }

    public void UpdateQuestJournalList()
    {
        FindObjectOfType<QuestManager>().SortQuestList(FindObjectOfType<QuestManager>().GetQuestPool());
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }

        foreach (Quest quest in FindObjectOfType<QuestManager>().GetQuestsByStatus(Quest.Status.Active))
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
        foreach (Quest quest in FindObjectOfType<QuestManager>().GetQuestPool())
        {
            if (quest.State == Quest.Status.Completed || quest.State == Quest.Status.Failed)
            {
                GameObject newQuestUI = Instantiate(questUI, transform);
                newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
            }
        }
    }
}
