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
    private List<QuestData> questDataArchive;
    private List<QuestTimerData> questTimerDatas;
    private SettingsData settingsData;
    private PopulationManagerData populationManagerData;

    public SaveData(GuildMemberData _heroData, GuildhallData _guildhallData, List<GuildMemberData> _guildMemberDatas, List<QuestData> _questDataPool, List<QuestData> _questDataArchive, List<QuestTimerData> _questTimerDatas, SettingsData _settingsData, PopulationManagerData _populationManagerData)
    {
        heroData = _heroData;
        guildhallData = _guildhallData;
        guildMemberDatas = _guildMemberDatas;
        questDataPool = _questDataPool;
        questDataArchive = _questDataArchive;
        questTimerDatas = _questTimerDatas;
        settingsData = _settingsData;
        populationManagerData = _populationManagerData;
    }

    public void Load()
    {
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        hero.person = heroData.person;
        hero.Vocation = heroData.adventurer;
        Sprite _heroAvatar = Resources.Load<Sprite>("Sprites/Avatars/" + heroData.person.gender + "/" + heroData.avatarSpriteName);
        hero.Avatar = _heroAvatar;
        hero.Id = heroData.id;
        hero.Hitpoints = heroData.hitpoints;
        hero.MaxHitpoints = heroData.maxHitpoints;
        hero.Experience = heroData.experience;
        hero.Level = heroData.level;
        hero.IsAvailable = heroData.isAvailable;
        hero.bio = heroData.bio;
        GameObject.FindObjectOfType<PopulationManager>().GuildMembers.Add(hero);

        Guildhall guildhall = GameObject.FindObjectOfType<Guildhall>();
        guildhall.Gold = guildhallData.gold;
        guildhall.Iron = guildhallData.iron;
        guildhall.Wood = guildhallData.wood;
        guildhall.Weapons = guildhallData.weapons;
        guildhall.Renown = guildhallData.renown;
        guildhall.renownThreshold = guildhallData.renownThreshold;

        foreach (GuildMemberData guildMemberData in guildMemberDatas)
        {
            // PopulationManager needs to handle this because GuildMember inherits Monobehaviour so must be instantiated.
            GameObject.FindObjectOfType<PopulationManager>().LoadGuildMember(guildMemberData);
        }

        List<Quest> _questPool = new List<Quest>();
        foreach (QuestData questData in questDataPool)
        {
            Quest newQuest = LoadQuest(questData);
            _questPool.Add(newQuest);
        }
        GameObject.FindObjectOfType<QuestManager>().SetQuestPool(_questPool);

        List<Quest> _questArchive = new List<Quest>();
        foreach (QuestData questData in questDataArchive)
        {
            Quest newQuest = LoadQuest(questData);
            _questArchive.Add(newQuest);
        }
        GameObject.FindObjectOfType<QuestManager>().SetQuestArchive(_questArchive);

        foreach (QuestTimerData questTimerData in questTimerDatas)
        {
            // QuestManager needs to handle this because QuestTimer inherits Monobehaviour so must be instantiated.
            GameObject.FindObjectOfType<QuestManager>().LoadQuestTimer(questTimerData);
        }

        GameObject.FindObjectOfType<PopulationManager>().recoveryStartTime = populationManagerData.recoveryStartTime;

        if (settingsData != null)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = settingsData.musicVolume;
            GameObject.Find("SoundManager").GetComponent<AudioSource>().volume = settingsData.soundVolume;
        }
    }

    private Quest LoadQuest(QuestData _questData)
    {
        Quest questToLoad = new Quest();
        questToLoad.questName = _questData.questName;
        questToLoad.contractor = _questData.contractor;
        questToLoad.description = _questData.description;
        questToLoad.commencement = _questData.commencement;
        questToLoad.completion = _questData.completion;
        questToLoad.id = _questData.id;
        questToLoad.difficulty = _questData.difficulty;
        questToLoad.time = _questData.time;
        questToLoad.questInstanceId = _questData.questInstanceId;
        questToLoad.Reward = _questData.Reward;
        if (_questData.guildMemberId != 0)
        {
            questToLoad.GuildMember = GameObject.FindObjectOfType<PopulationManager>().FindGuildMemberById(_questData.guildMemberId);
        }
        questToLoad.State = _questData.State;
        questToLoad.Incidents = _questData.Incidents;
        questToLoad.startTime = _questData.startTime;
        return questToLoad;
    }
}