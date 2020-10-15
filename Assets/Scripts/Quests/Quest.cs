using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Failed }

    public string questName, contractor, description, commencement, completion;
    public int id, level, time;
    public int questInstanceId;
    public Reward Reward;
    public GuildMember GuildMember;
    public Status State;
    public List<Incident> Incidents;
    public DateTime startTime;

    public void Init()
    {
        Reward = new Reward(level);
        Incidents = new List<Incident>();
        questInstanceId = Helpers.GenerateId();
        SetTime();
        State = Status.New;
    }

    private void SetTime()
    {
        switch (level)
        {
            case (1):
                time = 60;
                break;
            case (5):
                time = 300;
                break;
            case (10):
                time = 900;
                break;
            case (15):
                time = 1800;
                break;
            case (20):
                time = 3600;
                break;
            case (25):
                time = 7200;
                break;
            case (30):
                time = 21600;
                break;
            case (35):
                time = 43200;
                break;
            default:
                time = 60;
                break;
        }
    }
}
