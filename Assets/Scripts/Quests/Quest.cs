using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Archived }

    public string questName, contractor, description, commencement, completion;
    public int id, difficulty, time;
    public QuestReward Reward;
    public GuildMember GuildMember;
    public Status State;
    public List<Incident> Incidents;
    public DateTime startTime;

    public void Init()
    {
        Reward = new QuestReward(difficulty);
        Incidents = new List<Incident>();
        State = Status.New;
    }
}
