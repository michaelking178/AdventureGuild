using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    private GuildMemberData heroData;
    private GuildhallData guildhallData;
    private List<GuildMemberData> guildMemberDatas;
    private List<QuestData> questDataPool;
    private List<QuestTimerData> questTimerDatas;

    public SaveData(GuildMemberData _heroData, GuildhallData _guildhallData, List<GuildMemberData> _guildMemberDatas, List<QuestData> _questDataPool, List<QuestTimerData> _questTimerDatas)
    {
        heroData = _heroData;
        guildhallData = _guildhallData;
        guildMemberDatas = _guildMemberDatas;
        questDataPool = _questDataPool;
        questTimerDatas = _questTimerDatas;
    }

    public void Load()
    {
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        hero.person = heroData.person;
        hero.SetVocation(heroData.vocation);
        Sprite _heroAvatar = Resources.Load<Sprite>("Sprites/Avatars/" + heroData.person.gender + "/" + heroData.avatarSpriteName);
        hero.SetAvatar(_heroAvatar);
        hero.SetHealth(heroData.health);
        hero.SetExp(heroData.experience);
        hero.SetLevel(heroData.level);
        hero.IsAvailable(heroData.isAvailable);
        UnityEngine.Object.FindObjectOfType<PopulationManager>().GuildMembers.Add(hero);

        Guildhall guildhall = UnityEngine.Object.FindObjectOfType<Guildhall>();
        guildhall.Gold = guildhallData.gold;
        guildhall.Iron = guildhallData.iron;
        guildhall.Wood = guildhallData.wood;
        guildhall.Weapons = guildhallData.weapons;
        guildhall.Renown = guildhallData.renown;

        foreach (GuildMemberData guildMemberData in guildMemberDatas)
        {
            // PopulationManager needs to handle this because GuildMember inherits Monobehaviour so must be instantiated.
            UnityEngine.Object.FindObjectOfType<PopulationManager>().LoadGuildMember(guildMemberData);
        }

        List<Quest> questList = new List<Quest>();
        foreach (QuestData questData in questDataPool)
        {
            Quest newQuest = new Quest();
            newQuest.questName = questData.questName;
            newQuest.contractor = questData.contractor;
            newQuest.description = questData.description;
            newQuest.commencement = questData.commencement;
            newQuest.completion = questData.completion;
            newQuest.id = questData.id;
            newQuest.difficulty = questData.difficulty;
            newQuest.time = questData.time;
            newQuest.Reward = questData.Reward;
            if (questData.guildMemberData != null)
            {
                newQuest.GuildMember = UnityEngine.Object.FindObjectOfType<PopulationManager>().FindGuildMemberById(questData.guildMemberData.id);
            }
            newQuest.State = questData.State;
            newQuest.Incidents = questData.Incidents;
            newQuest.startTime = questData.startTime;
            questList.Add(newQuest);
        }
        UnityEngine.Object.FindObjectOfType<QuestManager>().SetQuestPool(questList);

        foreach (QuestTimerData questTimerData in questTimerDatas)
        {
            // QuestManager needs to handle this because QuestTimer inherits Monobehaviour so must be instantiated.
            UnityEngine.Object.FindObjectOfType<QuestManager>().LoadQuestTimer(questTimerData);
        }
    }
}
