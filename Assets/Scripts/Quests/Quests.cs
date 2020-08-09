using UnityEngine;

[System.Serializable]
public class Quests
{
    public Quest[] quests;

    public Quest GetRandomQuest()
    {
        Quest quest = quests[Random.Range(0, quests.Length)];
        quest.Init();
        return quest;
    }

    public Quest GetQuestById(int _id)
    {
        foreach (Quest quest in quests)
        {
            if (quest.id == _id)
            {
                return quest;
            }    
        }
        Debug.Log("Quests.cs could not find the requested Quest with ID " + _id);
        return null;
    }
}
