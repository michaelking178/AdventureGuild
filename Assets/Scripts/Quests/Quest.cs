using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Failed }
    public string questName, contractor, description, commencement, completion, trophyDescription;
    public int id, level, time, nextQuestID;
    public bool continuingQuest, questChain;
    public int questInstanceId;
    public Reward Reward;
    public GuildMember GuildMember;
    public List<Incident> Incidents;
    public DateTime startTime;
    public QuestTimer Timer;
    public Status State;

    public bool RewardBoosted { get; set; } = false;

    public void Init()
    {
        Incidents = new List<Incident>();
        questInstanceId = Helpers.GenerateId();
        State = Status.New;
        SetTime();
        Reward = new Reward(level);
    }

    private void SetTime()
    {
        switch (level)
        {
            case (1):
                time = 60;
                break;
            case (5):
                time = 600;
                break;
            case (10):
                time = 1800;
                break;
            case (15):
                time = 3600;
                break;
            case (20):
                time = 7200;
                break;
            default:
                time = 60;
                break;
        }
    }
}
