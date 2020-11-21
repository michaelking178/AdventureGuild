using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string ApplicationVersion;
    private GuildMemberData heroData;
    private GuildhallData guildhallData;
    private List<GuildMemberData> guildMemberDatas;
    private List<QuestData> questDataPool;
    private List<QuestData> questDataArchive;
    private List<QuestTimerData> questTimerDatas;
    private SettingsData settingsData;
    private PopulationManagerData populationManagerData;
    private QuestManagerData questManagerData;
    private ConstructionManagerData constructionManagerData;

    public SaveData(
        GuildMemberData _heroData, 
        GuildhallData _guildhallData, 
        List<GuildMemberData> _guildMemberDatas, 
        List<QuestData> _questDataPool, 
        List<QuestData> _questDataArchive, 
        List<QuestTimerData> _questTimerDatas, 
        SettingsData _settingsData, 
        PopulationManagerData _populationManagerData, 
        QuestManagerData _questManagerData,
        ConstructionManagerData _constructionManagerData
        )
    {
        ApplicationVersion = Application.version;
        heroData = _heroData;
        guildhallData = _guildhallData;
        guildMemberDatas = _guildMemberDatas;
        questDataPool = _questDataPool;
        questDataArchive = _questDataArchive;
        questTimerDatas = _questTimerDatas;
        settingsData = _settingsData;
        populationManagerData = _populationManagerData;
        questManagerData = _questManagerData;
        constructionManagerData = _constructionManagerData;
    }

    public void Load()
    {
        if (settingsData != null)
        {
            LoadSettings();
        }
        LoadHero();
        LoadGuildhall();
        LoadPopulationManager();
        LoadQuestManager();
        LoadGuildMembers();
        LoadQuestPool();
        LoadQuestArchive();
        LoadQuestTimers();
        LoadConstructionManager();
    }

    private void LoadSettings()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = settingsData.musicVolume;
        GameObject.Find("SoundManager").GetComponent<AudioSource>().volume = settingsData.soundVolume;
        GameObject.Find("Population Manager").GetComponent<PopulationManager>().DebugBoostEnabled = settingsData.debugBoostEnabled;
    }

    private void LoadHero()
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
        hero.Bio = heroData.bio;
        GameObject.FindObjectOfType<PopulationManager>().GuildMembers.Add(hero);
    }

    private void LoadGuildhall()
    {
        Guildhall guildhall = GameObject.FindObjectOfType<Guildhall>();
        guildhall.Gold = guildhallData.gold;
        guildhall.Iron = guildhallData.iron;
        guildhall.Wood = guildhallData.wood;
        guildhall.Renown = guildhallData.renown;
        guildhall.RenownLevel = guildhallData.renownLevel;
        guildhall.GoldIncome = guildhallData.goldIncome;
        guildhall.IronIncome = guildhallData.ironIncome;
        guildhall.WoodIncome = guildhallData.woodIncome;
        guildhall.StartTime = guildhallData.startTime;
    }

    private void LoadPopulationManager()
    {
        PopulationManager populationManager = GameObject.FindObjectOfType<PopulationManager>();
        populationManager.RecoveryStartTime = populationManagerData.recoveryStartTime;
        populationManager.PopulationCap = populationManagerData.populationCap;
        populationManager.ArtisansEnabled = populationManagerData.artisansEnabled;

        if (populationManagerData.recruitStartTime != null)
        {
            populationManager.RecruitStartTime = populationManagerData.recruitStartTime;
        }
        else
        {
            populationManager.RecruitStartTime = DateTime.MinValue;
        }
    }

    private void LoadQuestManager()
    {
        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
        questManager.CombatUnlocked = questManagerData.CombatUnlocked;
        questManager.EspionageUnlocked = questManagerData.EspionageUnlocked;
        questManager.DiplomacyUnlocked = questManagerData.DiplomacyUnlocked;
    }

    private void LoadGuildMembers()
    {
        foreach (GuildMemberData guildMemberData in guildMemberDatas)
        {
            // PopulationManager needs to handle this because GuildMember inherits Monobehaviour so must be instantiated.
            GameObject.FindObjectOfType<PopulationManager>().LoadGuildMember(guildMemberData);
        }
    }

    private void LoadQuestPool()
    {
        List<Quest> _questPool = new List<Quest>();
        foreach (QuestData questData in questDataPool)
        {
            Quest newQuest = LoadQuest(questData);
            _questPool.Add(newQuest);
        }
        GameObject.FindObjectOfType<QuestManager>().SetQuestPool(_questPool);
    }

    private void LoadQuestArchive()
    {
        List<Quest> _questArchive = new List<Quest>();
        foreach (QuestData questData in questDataArchive)
        {
            Quest newQuest = LoadQuest(questData);
            _questArchive.Add(newQuest);
        }
        GameObject.FindObjectOfType<QuestManager>().SetQuestArchive(_questArchive);
    }

    private void LoadQuestTimers()
    {
        foreach (QuestTimerData questTimerData in questTimerDatas)
        {
            // QuestManager needs to handle this because QuestTimer inherits Monobehaviour so must be instantiated.
            GameObject.FindObjectOfType<QuestManager>().LoadQuestTimer(questTimerData);
        }
    }

    private void LoadConstructionManager()
    {
        ConstructionManager constructionManager = GameObject.FindObjectOfType<ConstructionManager>();
        if (constructionManagerData == null)
        {
            Debug.Log("SAVEDATA.CS: No ConstructionManagerData found!");
        }
        else
        {
            if (!constructionManagerData.UnderConstruction)
            {
                constructionManager.ConstructionJob = null;
            }
            else
            {
                Upgrade[] upgrades = GameObject.FindObjectsOfType<Upgrade>();
                foreach (Upgrade upgrade in upgrades)
                {
                    if (upgrade.name == constructionManagerData.ConstructionJobName)
                    {
                        constructionManager.ConstructionJob = upgrade;
                    }
                }
                foreach (int id in constructionManagerData.ArtisanIDs)
                {
                    GuildMember artisan = GameObject.FindObjectOfType<PopulationManager>().FindGuildMemberById(id);
                    if (artisan == null)
                    {
                        Debug.Log($"SAVEDATA.CS: Cannot find Artisan with ID #{id}");
                    }
                    else
                    {
                        constructionManager.AddArtisan(artisan);
                    }
                }
                constructionManager.StartTime = constructionManagerData.StartTime;
            }
            constructionManager.ConstructionName = constructionManagerData.ConstructionJobName;
            constructionManager.UnderConstruction = constructionManagerData.UnderConstruction;
        }
    }

    private Quest LoadQuest(QuestData _questData)
    {
        Quest questToLoad = new Quest();
        questToLoad.questName = _questData.questName;
        questToLoad.contractor = _questData.contractor;
        questToLoad.skill = _questData.skill;
        questToLoad.faction = _questData.faction;
        questToLoad.description = _questData.description;
        questToLoad.commencement = _questData.commencement;
        questToLoad.completion = _questData.completion;
        questToLoad.id = QuestIDCompatibility(_questData.id);
        questToLoad.level = _questData.level;
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
        questToLoad.QuestSkill = _questData.questSkill;
        questToLoad.QuestFaction = _questData.questFaction;
        return questToLoad;
    }

    private int QuestIDCompatibility(int _id)
    {
        switch (_id)
        {
            case 001: return 001001;
            case 002: return 001002;
            case 003: return 001003;
            case 004: return 001004;
            case 005: return 001005;
            case 006: return 001006;
            case 007: return 001007;
            case 008: return 001008;
            case 009: return 001009;
            case 010: return 001010;
            case 011: return 001011;
            case 012: return 001012;
            case 013: return 001013;
            case 014: return 001014;
            case 015: return 001015;

            case 016: return 005001;
            case 017: return 005002;
            case 018: return 005003;
            case 019: return 005004;
            case 020: return 005005;
            case 021: return 005006;
            case 022: return 005007;
            case 023: return 005008;
            case 024: return 005009;
            case 025: return 005010;
            case 026: return 005011;
            case 027: return 005012;
            case 028: return 005013;
            case 029: return 005014;
            case 030: return 005015;

            case 031: return 010001;
            case 032: return 010002;
            case 033: return 010003;
            case 034: return 010004;
            case 035: return 010005;
            case 036: return 010006;
            case 037: return 010007;
            case 038: return 010008;
            case 039: return 010009;
            case 040: return 010010;
            case 041: return 010011;
            case 042: return 010012;
            case 043: return 010013;
            case 044: return 010014;
            case 045: return 010015;

            case 046: return 015001;
            case 047: return 015002;
            case 048: return 015003;
            case 049: return 015004;
            case 050: return 015005;
            case 051: return 015006;
            case 052: return 015007;
            case 053: return 015008;
            case 054: return 015009;
            case 055: return 015010;
            case 056: return 015011;
            case 057: return 015012;
            case 058: return 015013;
            case 059: return 015014;
            case 060: return 015015;

            default: return _id;
        }
    }
}