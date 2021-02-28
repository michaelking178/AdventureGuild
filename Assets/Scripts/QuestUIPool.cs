using UnityEngine;

public class QuestUIPool : MonoBehaviour
{
    public QuestUI[] QuestUIs = new QuestUI[100];

    public QuestUI GetNextAvailableQuestUI()
    {
        for (int i = 0; i < QuestUIs.Length; i++)
        {
            if (QuestUIs[i].GetQuest() == null)
                return QuestUIs[i];
        }
        return null;
    }
}
