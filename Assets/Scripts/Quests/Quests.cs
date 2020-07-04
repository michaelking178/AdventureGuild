using UnityEngine;

[System.Serializable]
public class Quests
{
    public Quest[] quests;

    public Quest GetRandomQuest()
    {
        Quest quest = quests[Random.Range(0, quests.Length)];
        return quest;
    }
}
