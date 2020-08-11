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
    private SettingsData settingsData;
    private PopulationManagerData populationManagerData;

    public SaveData(GuildMemberData _heroData, GuildhallData _guildhallData, List<GuildMemberData> _guildMemberDatas, List<QuestData> _questDataPool, List<QuestTimerData> _questTimerDatas, SettingsData _settingsData, PopulationManagerData _populationManagerData)
    {
        heroData = _heroData;
        guildhallData = _guildhallData;
        guildMemberDatas = _guildMemberDatas;
        questDataPool = _questDataPool;
        questTimerDatas = _questTimerDatas;
        settingsData = _settingsData;
        populationManagerData = _populationManagerData;
    }

    public void Load()
    {
        QuestManager questManager = UnityEngine.Object.FindObjectOfType<QuestManager>();
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        Guildhall guildhall = UnityEngine.Object.FindObjectOfType<Guildhall>();
        PopulationManager populationManager = UnityEngine.Object.FindObjectOfType<PopulationManager>();

        hero.person = heroData.person;
        hero.Vocation = heroData.vocation;
        Sprite _heroAvatar = Resources.Load<Sprite>("Sprites/Avatars/" + heroData.person.gender + "/" + heroData.avatarSpriteName);
        hero.Avatar = _heroAvatar;
        hero.Id = heroData.id;
        hero.Hitpoints = heroData.hitpoints;
        hero.MaxHitpoints = heroData.maxHitpoints;
        hero.Experience = heroData.experience;
        hero.Level = heroData.level;
        hero.IsAvailable = heroData.isAvailable;
        populationManager.GuildMembers.Add(hero);

        guildhall.Gold = guildhallData.gold;
        guildhall.Iron = guildhallData.iron;
        guildhall.Wood = guildhallData.wood;
        guildhall.Weapons = guildhallData.weapons;
        guildhall.Renown = guildhallData.renown;

        foreach (GuildMemberData guildMemberData in guildMemberDatas)
        {
            // PopulationManager needs to handle this because GuildMember inherits Monobehaviour so must be instantiated.
            populationManager.LoadGuildMember(guildMemberData);
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
            newQuest.questInstanceId = questData.questInstanceId;
            newQuest.Reward = questData.Reward;
            if (questData.guildMemberId != 0)
            {
                newQuest.GuildMember = populationManager.FindGuildMemberById(questData.guildMemberId);
            }
            newQuest.State = questData.State;
            newQuest.Incidents = questData.Incidents;
            newQuest.startTime = questData.startTime;
            questList.Add(newQuest);
        }
        questManager.SetQuestPool(questList);

        foreach (QuestTimerData questTimerData in questTimerDatas)
        {
            // QuestManager needs to handle this because QuestTimer inherits Monobehaviour so must be instantiated.
            questManager.LoadQuestTimer(questTimerData);
        }

        populationManager.recoveryStartTime = populationManagerData.recoveryStartTime;

        if (settingsData != null)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = settingsData.musicVolume;
            GameObject.Find("SoundManager").GetComponent<AudioSource>().volume = settingsData.soundVolume;
        }
    }
}