using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Archived }

    public string questName, contractor, description, commencement, completion;
    public int id, difficulty, time;
    public int questInstanceId;
    public Reward Reward;
    public GuildMember GuildMember;
    public Status State;
    public List<Incident> Incidents;
    public DateTime startTime;

    public void Init()
    {
        Reward = new Reward(difficulty);
        Incidents = new List<Incident>();
        questInstanceId = Helpers.GenerateId();
        State = Status.New;
    }
}
