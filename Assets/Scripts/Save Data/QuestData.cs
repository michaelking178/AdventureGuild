using System;
using System.Collections.Generic;

[Serializable]
public class QuestData
{
    public string questName, contractor, description, commencement, completion;
    public int id, level, time, nextQuestID;
    public bool continuingQuest, questChain;
    public int questInstanceId;
    public Reward Reward;
    public int guildMemberId = 0;
    public Quest.Status State;
    public List<Incident> Incidents;
    public DateTime startTime;
    public bool rewardBoosted;

    public QuestData(Quest quest)
    {
        questName = quest.questName;
        contractor = quest.contractor;
        description = quest.description;
        commencement = quest.commencement;
        completion = quest.completion;
        id = quest.id;
        level = quest.level;
        time = quest.time;
        continuingQuest = quest.continuingQuest;
        questChain = quest.questChain;
        nextQuestID = quest.nextQuestID;
        questInstanceId = quest.questInstanceId;
        Reward = quest.Reward;
        if (quest.GuildMember != null)
        {
            guildMemberId = quest.GuildMember.Id;
        }
        State = quest.State;
        Incidents = quest.Incidents;
        startTime = quest.startTime;
        rewardBoosted = quest.RewardBoosted;
    }
}
