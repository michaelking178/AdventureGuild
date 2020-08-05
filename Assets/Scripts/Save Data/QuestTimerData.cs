using System;

[Serializable]
public class QuestTimerData
{
    public int questId;
    public float timeLimit;
    public DateTime startTime;
    public bool isTiming;
    public bool questFinished;
    public int incidentTimer;

    public QuestTimerData(QuestTimer questTimer)
    {
        questId = questTimer.GetQuest().id;
        timeLimit = questTimer.TimeLimit;
        startTime = questTimer.StartTime;
        isTiming = questTimer.IsTiming;
        questFinished = questTimer.QuestFinished;
        incidentTimer = questTimer.IncidentTimer;
    }
}
