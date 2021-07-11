using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public enum Status { New, Active, Completed, Failed }
    public enum Skill { None, Combat, Espionage, Diplomacy }
    public enum Faction { None, MagesGuild, MerchantsGuild, RoyalPalace }

    public string questName, contractor, skill, faction, description, commencement, completion, trophyDescription;
    public int id, level, time, nextQuestID;
    public bool continuingQuest, questChain;
    public int questInstanceId;
    public Reward Reward;
    public GuildMember GuildMember;
    public List<Incident> Incidents;
    public DateTime startTime;
    public QuestTimer Timer;
    public Status State;
    public Skill QuestSkill;
    public Faction QuestFaction;

    public bool RewardBoosted { get; set; } = false;

    public void Init()
    {
        Incidents = new List<Incident>();
        questInstanceId = Helpers.GenerateId();
        State = Status.New;
        SetTime();
        SetSkill();
        SetFaction();
        Reward = new Reward(level, QuestSkill);
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

    private void SetSkill()
    {
        switch (skill)
        {
            case "Combat":
                QuestSkill = Skill.Combat;
                break;
            case "Espionage":
                QuestSkill = Skill.Espionage;
                break;
            case "Diplomacy":
                QuestSkill = Skill.Diplomacy;
                break;
            default:
                QuestSkill = Skill.None;
                break;
        }
    }

    public string GetSkillString()
    {
        return skill;
    }

    private void SetFaction()
    {
        switch (faction)
        {
            case "MagesGuild":
                QuestFaction = Faction.MagesGuild;
                break;
            case "MerchantsGuild":
                QuestFaction = Faction.MerchantsGuild;
                break;
            case "RoyalPalace":
                QuestFaction = Faction.RoyalPalace;
                break;
            default:
                QuestFaction = Faction.None;
                break;
        }
    }

    public string GetFactionString()
    {
        switch (QuestFaction)
        {
            case Faction.MagesGuild:
                return "Mages' Guild";
            case Faction.MerchantsGuild:
                return "Merchants' Guild";
            case Faction.RoyalPalace:
                return "Royal Palace";
            default:
                return null;
        }
    }
}
