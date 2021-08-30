using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    [SerializeField]
    private GuildMember guildMemberPrefab;

    [SerializeField]
    private TextAsset quipsJson;

    public List<Sprite> maleAvatars;
    public List<Sprite> femaleAvatars;
    public Sprite defaultMaleAvatar;
    public Sprite defaultFemaleAvatar;

    [Header("Guild Members")]
    public List<GuildMember> GuildMembers = new List<GuildMember>();
    public int PopulationCap { get; set; } = 10;
    public DateTime RecoveryStartTime = DateTime.MinValue;
    public DateTime RecruitStartTime;

    public bool AdventurersEnabled = false;
    public bool ArtisansEnabled = false;
    public int AdventurerHPUpgradeLevel {get; set; } = 0;
    public int PeasantIncomeUpgradeLevel { get; set; } = 0;

    private int hitpointRecovery = 5;
    private float recoveryTimer = 60.0f;
    private float recoveryTime;
    private int recoveryQueue;
    private float recruitTimer = 3600.0f;
    private float recruitTime;
    private int recruitQueue = 0;
    private int recruitQueueLimit = 10;

    private LevelManager levelManager;
    private NotificationManager notificationManager;
    private Guildhall guildhall;
    private NameGenerator nameGenerator;
    private Quips quips;

    public delegate void OnPopulationManagerAction(int value);
    public static event OnPopulationManagerAction OnRecruit;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        notificationManager = FindObjectOfType<NotificationManager>();
        guildhall = FindObjectOfType<Guildhall>();
        nameGenerator = FindObjectOfType<NameGenerator>();
        quips = JsonUtility.FromJson<Quips>(quipsJson.text);
        if (RecoveryStartTime == DateTime.MinValue)
            RecoveryStartTime = DateTime.Now;
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        RecoverHitpoints();
        PassiveRecruitment();
    }

    public void CreateGuildMember()
    {
        if (GuildMembers.Count < PopulationCap)
        {
            string firstName;
            string lastName = nameGenerator.LastName();
            string quip = quips.quips[UnityEngine.Random.Range(0, quips.quips.Length)];
            Sprite avatar;
            int gender = UnityEngine.Random.Range(0, 2);
            if (gender == 0)
            {
                firstName = nameGenerator.FirstName("male");
                avatar = maleAvatars[UnityEngine.Random.Range(0, maleAvatars.Count)];
            }
            else
            {
                firstName = nameGenerator.FirstName("female");
                avatar = femaleAvatars[UnityEngine.Random.Range(0, femaleAvatars.Count)];
            }
            Person newPerson = new Person(gender, firstName, lastName);
            GuildMember newMember = Instantiate(guildMemberPrefab, transform);
            newMember.Init(newPerson);
            newMember.Quip = quip;
            newMember.Avatar = avatar;
            GuildMembers.Add(newMember);
            notificationManager.CreateNotification($"{newMember.person.name} has heard of your Renown and joined the Adventure Guild!", Notification.Spirit.Good);
            ApplyRecruitmentAchievementProgress();

            // Event for Challenges
            OnRecruit?.Invoke(1);
        }
    }

    public void LoadGuildMember(GuildMemberData guildMemberData)
    {
        GuildMember newMember = Instantiate(guildMemberPrefab, transform);
        newMember.person = guildMemberData.person;
        newMember.Quip = guildMemberData.quip;
        if (guildMemberData.avatarSpriteName != null && guildMemberData.avatarSpriteName != "")
        {
            Sprite avatar = Resources.Load<Sprite>("Sprites/Avatars/" + guildMemberData.person.gender + "/" + guildMemberData.avatarSpriteName);
            newMember.Avatar = avatar;
        }
        newMember.Id = guildMemberData.id;
        newMember.Experience = guildMemberData.experience;
        newMember.Hitpoints = guildMemberData.hitpoints;
        newMember.MaxHitpoints = guildMemberData.maxHitpoints;
        newMember.Level = guildMemberData.level;

        if (guildMemberData.adventurer != null)
            newMember.Vocation = guildMemberData.adventurer;
        else
            newMember.Vocation = guildMemberData.vocation;

        newMember.IsAvailable = guildMemberData.isAvailable;
        newMember.Bio = guildMemberData.bio;
        GuildMembers.Add(newMember);
    }

    public List<GuildMember> GetAvailableAdventurers()
    {
        List<GuildMember> adventurers = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Adventurer && guildMember.IsAvailable && !guildMember.IsIncapacitated)
                adventurers.Add(guildMember);
        }
        return adventurers;
    }

    public List<GuildMember> GetAvailableArtisans()
    {
        List<GuildMember> artisans = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Artisan && guildMember.IsAvailable)
                artisans.Add(guildMember);
        }
        return artisans;
    }

    public List<GuildMember> Adventurers()
    {
        List<GuildMember> adventurers = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Adventurer)
                adventurers.Add(guildMember);
        }
        return adventurers;
    }

    public List<GuildMember> Artisans()
    {
        List<GuildMember> artisans = new List<GuildMember>();
        foreach (GuildMember guildmember in GuildMembers)
        {
            if (guildmember != null && guildmember.Vocation is Artisan)
                artisans.Add(guildmember);
        }
        return artisans;
    }

    public List<GuildMember> Peasants()
    {
        List<GuildMember> peasants = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Peasant)
                peasants.Add(guildMember);
        }
        return peasants;
    }

    public GuildMember FindGuildMemberById(int _id)
    {
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Id == _id)
                return guildMember;
        }
        return null;
    }

    public void SetPopulationCap(int _populationCap)
    {
        PopulationCap = _populationCap;
    }

    public void RemoveGuildMember(GuildMember _guildMember)
    {
        if (_guildMember.Vocation is Peasant peasant)
            guildhall.AdjustIncome(peasant.IncomeResource, -peasant.Income);

        GuildMembers.Remove(_guildMember);
    }

    public void SortGuildMembersByLevel()
    {
        List<GuildMember> sorted = GuildMembers.OrderByDescending(gm => gm.Level).ToList();
        GuildMembers = sorted;
    }

    public void EnableAdventurers()
    {
        AdventurersEnabled = true;
    }

    public void EnableArtisans()
    {
        ArtisansEnabled = true;
    }

    public void ApplyAdventurerHPUpgrade(int effect)
    {
        AdventurerHPUpgradeLevel = effect;
        foreach (GuildMember guildmember in Adventurers())
        {
            guildmember.CalculateMaxHP();
        }
    }

    public void ApplyPeasantIncomeUpgrade(int effect)
    {
        PeasantIncomeUpgradeLevel = effect;
        foreach (GuildMember guildmember in Peasants())
        {
            if (guildmember.Vocation is Peasant peasant)
                guildmember.CalculatePeasantIncome(peasant);
        }
    }

    private void RecoverHitpoints()
    {
        if (RecoveryStartTime == DateTime.MinValue) return;

        TimeSpan difference = DateTime.Now - RecoveryStartTime;
        recoveryTime = (float)difference.TotalSeconds;
        recoveryQueue = Mathf.FloorToInt(recoveryTime / recoveryTimer);
        for (int i = 0; i < recoveryQueue; i++)
        {
            foreach (GuildMember guildMember in GuildMembers)
            {
                if (guildMember.Hitpoints != guildMember.MaxHitpoints && guildMember.IsAvailable)
                    guildMember.AdjustHitpoints(hitpointRecovery);
            }
        }
        if (DateTime.Now > RecoveryStartTime.AddSeconds(recoveryTimer))
            RecoveryStartTime = DateTime.Now;
    }

    private void PassiveRecruitment()
    {
        if (RecruitStartTime == DateTime.MinValue) return;

        TimeSpan difference = DateTime.Now - RecruitStartTime;
        recruitTime = (float)difference.TotalSeconds;
        recruitQueue = Mathf.FloorToInt(recruitTime / recruitTimer);
        if (recruitQueue > recruitQueueLimit)
            recruitQueue = recruitQueueLimit;

        for (int i = 0; i < recruitQueue; i++)
            CheckForRecruit();

        if (DateTime.Now > RecruitStartTime.AddSeconds(recruitTimer))
            RecruitStartTime = DateTime.Now;

        recruitQueue = 0;
    }

    private void CheckForRecruit()
    {
        float odds;
        if (PopulationCap == 0)
            odds = 0;
        else
            odds = ((PopulationCap - GuildMembers.Count) * guildhall.Renown) / (float)(Levelling.RenownLevel[guildhall.RenownLevel] * PopulationCap);

        float roll = UnityEngine.Random.Range(0.01f, 1.0f);
        if (roll <= odds)
            CreateGuildMember();
    }

    private void ApplyRecruitmentAchievementProgress()
    {
        var googlePlay = FindObjectOfType<GPGSAuthentication>();

        if (GuildMembers.Count == 2)
            googlePlay.UnlockAchievement(GPGSIds.achievement_wolf_pack_of_2, 100.0f);
        else if (GuildMembers.Count == 5)
            googlePlay.UnlockAchievement(GPGSIds.achievement_humble_beginnings, 100.0f);
        else if (GuildMembers.Count == 25)
            googlePlay.UnlockAchievement(GPGSIds.achievement_adventure_inc, 100.0f);
        else if (GuildMembers.Count == 50)
            googlePlay.UnlockAchievement(GPGSIds.achievement_adventure_town, 100.0f);
        else if (GuildMembers.Count == 100)
            googlePlay.UnlockAchievement(GPGSIds.achievement_full_house, 100.0f);
    }
}
