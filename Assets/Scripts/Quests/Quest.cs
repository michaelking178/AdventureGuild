using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName, contractor, description;
    public int id, difficulty, time;
    public Timer Timer;
    public QuestReward Reward;
    public GuildMember GuildMember;

    public void Init()
    {
        Timer = new Timer();
        Timer.SetTime(time);
        Reward = new QuestReward(difficulty);
        Debug.Log(Reward.Gold);
    }
}
