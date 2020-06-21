using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject questUI;

    private QuestManager questManager;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        UpdateQuestList();
    }

    public void UpdateQuestList()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (Quest quest in questManager.GetQuests())
        {
            GameObject newQuestUI = Instantiate(questUI, transform);
            newQuestUI.GetComponent<QuestUI>().SetQuest(quest);
        }
    }
}
