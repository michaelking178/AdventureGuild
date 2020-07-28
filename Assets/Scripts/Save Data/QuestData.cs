using System;
using System.Collections.Generic;

[Serializable]
public class QuestData
{
    public string questName, contractor, description, commencement, completion;
    public int id, difficulty, time;
    public QuestReward Reward;
    public GuildMemberData guildMemberData;
    public Quest.Status State;
    public List<Incident> Incidents;
    public DateTime startTime;

    public QuestData(Quest quest)
    {
        questName = quest.questName;
        contractor = quest.contractor;
        description = quest.description;
        commencement = quest.commencement;
        completion = quest.completion;
        id = quest.id;
        difficulty = quest.difficulty;
        time = quest.time;
        Reward = quest.Reward;
        if (quest.GuildMember != null)
        {
            guildMemberData = new GuildMemberData(quest.GuildMember);
        }
        State = quest.State;
        Incidents = quest.Incidents;
        startTime = quest.startTime;
    }
}