using System.Timers;

[System.Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Archived }

    public string questName, contractor, description;
    public int id, difficulty, time;
    public System.Timers.Timer Timer;
    public QuestReward Reward;
    public GuildMember GuildMember;
    public Status State;

    public void Init()
    {
        Timer = new System.Timers.Timer(time);
        Reward = new QuestReward(difficulty);
        State = Status.New;
    }
}
