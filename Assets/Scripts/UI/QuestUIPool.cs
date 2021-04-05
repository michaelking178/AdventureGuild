using UnityEngine;

public class QuestUIPool : MonoBehaviour
{
    #region Data

    [SerializeField]
    private GameObject questUIPrefab;

    [SerializeField]
    private QuestUI[] questUIs = new QuestUI[100];

    private QuestManager questManager;

    #endregion

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public QuestUI GetNextAvailableQuestUI()
    {
        for (int i = 0; i < questUIs.Length; i++)
        {
            if (questUIs[i].GetQuest() == null)
                return questUIs[i];
        }
        return null;
    }

    private void FixedUpdate()
    {
        if (questUIs.Length < questManager.GetQuestPool().Count + questManager.GetQuestArchive().Count + 10)
        {
            QuestUI[] newQuestUIs = new QuestUI[questUIs.Length + 10];
            questUIs.CopyTo(newQuestUIs, 0);
            for (int i = 0; i < newQuestUIs.Length; i++)
            {
                if (newQuestUIs[i] == null)
                {
                    GameObject quest = Instantiate(questUIPrefab, transform);
                    newQuestUIs[i] = quest.GetComponent<QuestUI>();
                    newQuestUIs[i].gameObject.SetActive(false);
                }
            }
            questUIs = newQuestUIs;
        }
        else if (questUIs.Length > questManager.GetQuestPool().Count + questManager.GetQuestArchive().Count + 15)
        {
            QuestUI[] newQuestUIs = new QuestUI[questUIs.Length - 5];
            for (int i = 0; i < newQuestUIs.Length; i++)
            {
                newQuestUIs[i] = questUIs[i];
            }
            questUIs = newQuestUIs;
        }
    }
}
