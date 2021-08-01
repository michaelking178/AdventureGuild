using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string ApplicationVersion;
    public DateTime quitTime;
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
    private BoostData boostData;
    private TrophyManagerData trophyManagerData;

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
        ConstructionManagerData _constructionManagerData,
        BoostData _boostData,
        TrophyManagerData _trophyManagerData
        )
    {
        ApplicationVersion = Application.version;
        quitTime = DateTime.Now;
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
        boostData = _boostData;
        trophyManagerData = _trophyManagerData;
    }

    public void Load()
    {
        if (settingsData != null)
            LoadSettings();

        LoadHero();
        LoadGuildhall();
        LoadPopulationManager();
        LoadQuestManager();
        LoadGuildMembers();
        LoadQuestPool();
        LoadQuestArchive();
        LoadQuestTimers();
        LoadConstructionManager();
        LoadPopulationManagerQueueTimes();
        LoadBoostData();
        LoadTrophyManager();
    }

    public void LoadSettings()
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
        hero.Quip = heroData.quip;
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
        guildhall.MaxGold = guildhallData.maxGold;
        guildhall.MaxIron = guildhallData.maxIron;
        guildhall.MaxWood = guildhallData.maxWood;
        guildhall.MaxGoldIncome = guildhallData.maxGoldIncome;
        guildhall.MaxIronIncome = guildhallData.maxIronIncome;
        guildhall.MaxWoodIncome = guildhallData.maxWoodIncome;

        if (guildhall.MaxGold == 0)
            GuildhallCompat(guildhall);
    }

    private void LoadPopulationManager()
    {
        PopulationManager populationManager = GameObject.FindObjectOfType<PopulationManager>();
        populationManager.PopulationCap = populationManagerData.populationCap;
        populationManager.AdventurersEnabled = populationManagerData.adventurersEnabled;
        populationManager.ArtisansEnabled = populationManagerData.artisansEnabled;
    }

    private void LoadPopulationManagerQueueTimes()
    {
        PopulationManager populationManager = GameObject.FindObjectOfType<PopulationManager>();
        populationManager.RecoveryStartTime = populationManagerData.recoveryStartTime;
        populationManager.RecruitStartTime = populationManagerData.recruitStartTime;
    }

    private void LoadQuestManager()
    {
        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
        questManager.CombatUnlocked = questManagerData.CombatUnlocked;
        questManager.EspionageUnlocked = questManagerData.EspionageUnlocked;
        questManager.DiplomacyUnlocked = questManagerData.DiplomacyUnlocked;
        questManager.QuestsCompleted = questManagerData.QuestsCompleted;
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
            Debug.Log("SAVEDATA.CS: No ConstructionManagerData found!");
        else
        {
            if (!constructionManagerData.UnderConstruction)
                constructionManager.ConstructionJob = null;
            else
            {
                Upgrade[] upgrades = GameObject.FindObjectsOfType<Upgrade>();
                foreach (Upgrade upgrade in upgrades)
                {
                    if (upgrade.name == constructionManagerData.ConstructionJobName)
                        constructionManager.ConstructionJob = upgrade;
                }
                foreach (int id in constructionManagerData.ArtisanIDs)
                {
                    GuildMember artisan = GameObject.FindObjectOfType<PopulationManager>().FindGuildMemberById(id);
                    if (artisan == null)
                        Debug.Log($"SAVEDATA.CS: Cannot find Artisan with ID #{id}");
                    else
                        constructionManager.Artisans.Add(artisan);
                }
                constructionManager.StartTime = constructionManagerData.StartTime;
            }
            constructionManager.ConstructionName = constructionManagerData.ConstructionJobName;
            constructionManager.UnderConstruction = constructionManagerData.UnderConstruction;
        }
    }

    private void LoadBoostData()
    {
        float timePassed = (float)(DateTime.Now - quitTime).TotalSeconds;

        boostData.BoostRemaining -= timePassed;

        if (boostData.BoostRemaining < 0) boostData.BoostRemaining = 0;

        GameObject.FindObjectOfType<QuestRewardBoost>().BoostRemaining = boostData.BoostRemaining;
    }

    private void LoadTrophyManager()
    {
        if (trophyManagerData != null)
            GameObject.FindObjectOfType<TrophyManager>().Trophies = trophyManagerData.Trophies;
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
        questToLoad.id = _questData.id;
        questToLoad.level = _questData.level;
        questToLoad.time = _questData.time;
        questToLoad.continuingQuest = _questData.continuingQuest;
        questToLoad.questChain = _questData.questChain;
        questToLoad.nextQuestID = _questData.nextQuestID;
        questToLoad.questInstanceId = _questData.questInstanceId;
        questToLoad.Reward = _questData.Reward;

        if (_questData.guildMemberId != 0)
            questToLoad.GuildMember = GameObject.FindObjectOfType<PopulationManager>().FindGuildMemberById(_questData.guildMemberId);

        questToLoad.State = _questData.State;
        questToLoad.Incidents = _questData.Incidents;
        questToLoad.startTime = _questData.startTime;
        questToLoad.QuestSkill = _questData.questSkill;
        questToLoad.QuestFaction = _questData.questFaction;
        questToLoad.RewardBoosted = _questData.rewardBoosted;
        return questToLoad;
    }

    private void GuildhallCompat(Guildhall guildhall)
    {
        guildhall.MaxGold = 25000;
        guildhall.MaxIron = 10000;
        guildhall.MaxWood = 10000;
        guildhall.MaxGoldIncome = 250;
        guildhall.MaxIronIncome = 100;
        guildhall.MaxWoodIncome = 100;
    }
}
