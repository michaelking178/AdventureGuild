using UnityEngine;

public class QuestUIPool : MonoBehaviour
{
    [SerializeField]
    private QuestUI[] questUIs = new QuestUI[100];

    public QuestUI GetNextAvailableQuestUI()
    {
        for (int i = 0; i < questUIs.Length; i++)
        {
            if (questUIs[i].GetQuest() == null)
                return questUIs[i];
        }
        return null;
    }
}
