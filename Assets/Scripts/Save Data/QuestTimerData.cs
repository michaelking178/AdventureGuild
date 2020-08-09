using System;

[Serializable]
public class QuestTimerData
{
    public int questInstanceId;
    public float timeLimit;
    public DateTime startTime;
    public bool isTiming;
    public bool questFinished;
    public int incidentTimer;

    public QuestTimerData(QuestTimer questTimer)
    {
        questInstanceId = questTimer.GetQuest().questInstanceId;
        timeLimit = questTimer.TimeLimit;
        startTime = questTimer.StartTime;
        isTiming = questTimer.IsTiming;
        questFinished = questTimer.QuestFinished;
        incidentTimer = questTimer.IncidentTimer;
    }
}
