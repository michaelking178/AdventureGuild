using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField]
    private TextAsset maleNamesJson;

    [SerializeField]
    private TextAsset femaleNamesJson;

    [SerializeField]
    private TextAsset lastNamesJson;

    [SerializeField]
    private GuildMember guildMemberPrefab;

    public List<Sprite> maleAvatars;
    public List<Sprite> femaleAvatars;
    public Sprite defaultMaleAvatar;
    public Sprite defaultFemaleAvatar;

    [Header("Name Lists")]
    [SerializeField]
    private Names maleNames;

    [SerializeField]
    private Names femaleNames;

    [SerializeField]
    private Names lastNames;

    [Header("Guild Members")]
    public List<GuildMember> GuildMembers = new List<GuildMember>();
    public int PopulationCap { get; set; } = 5;
    public DateTime RecoveryStartTime;
    public DateTime RecruitStartTime;
    public bool ArtisansEnabled = false;

    private int hitpointRecovery = 5;
    private float recoveryTimer = 30.0f;
    private float recoveryTime;
    private int recoveryQueue;
    private float recruitTimer = 3600.0f;
    private float recruitTime;
    private int recruitQueue = 0;

    private NotificationManager notificationManager;
    private Guildhall guildhall;

    // Todo: DebugBoost testing tool can be removed later.
    public bool DebugBoostEnabled = false;
    public int DebugBoost = 3;

    private void Start()
    {
        notificationManager = FindObjectOfType<NotificationManager>();
        guildhall = FindObjectOfType<Guildhall>();
        maleNames = JsonUtility.FromJson<Names>(maleNamesJson.text);
        femaleNames = JsonUtility.FromJson<Names>(femaleNamesJson.text);
        lastNames = JsonUtility.FromJson<Names>(lastNamesJson.text);
        if (RecoveryStartTime == System.DateTime.MinValue) RecoveryStartTime = System.DateTime.Now;
        if (RecruitStartTime == System.DateTime.MinValue) RecruitStartTime = System.DateTime.Now;
    }

    private void FixedUpdate()
    {
        RecoverHitpoints();
        PassiveRecruitment();
    }

    public void CreateGuildMember()
    {
        if (GuildMembers.Count < PopulationCap)
        {
            string firstName;
            string lastName = lastNames.prefixes[UnityEngine.Random.Range(0, lastNames.prefixes.Length)] + lastNames.suffixes[UnityEngine.Random.Range(0, lastNames.suffixes.Length)];
            Sprite avatar;
            int gender = UnityEngine.Random.Range(0, 2);
            if (gender == 0)
            {
                firstName = maleNames.prefixes[UnityEngine.Random.Range(0, maleNames.prefixes.Length)] + maleNames.suffixes[UnityEngine.Random.Range(0, maleNames.suffixes.Length)];
                avatar = maleAvatars[UnityEngine.Random.Range(0, maleAvatars.Count)];
            }
            else
            {
                firstName = femaleNames.prefixes[UnityEngine.Random.Range(0, femaleNames.prefixes.Length)] + femaleNames.suffixes[UnityEngine.Random.Range(0, femaleNames.suffixes.Length)];
                avatar = femaleAvatars[UnityEngine.Random.Range(0, femaleAvatars.Count)];
            }
            Person newPerson = new Person(gender, firstName, lastName);
            GuildMember newMember = Instantiate(guildMemberPrefab, transform);
            newMember.Init(newPerson);
            newMember.Avatar = avatar;
            GuildMembers.Add(newMember);
            notificationManager.CreateNotification(string.Format("{0} has heard of your Renown and joined the Adventure Guild!", newMember.person.name), Notification.Spirit.Good);
        }
    }

    public void LoadGuildMember(GuildMemberData guildMemberData)
    {
        GuildMember newMember = Instantiate(guildMemberPrefab, transform);
        newMember.person = guildMemberData.person;
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
        {
            newMember.Vocation = guildMemberData.adventurer;
        }
        else
        {
            newMember.Vocation = guildMemberData.vocation;
        }
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
            {
                adventurers.Add(guildMember);
            }
        }
        return adventurers;
    }

    public List<GuildMember> GetAvailableArtisans()
    {
        List<GuildMember> artisans = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Artisan && guildMember.IsAvailable)
            {
                artisans.Add(guildMember);
            }
        }
        return artisans;
    }

    public List<GuildMember> Adventurers()
    {
        List<GuildMember> adventurers = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Adventurer)
            {
                adventurers.Add(guildMember);
            }
        }
        return adventurers;
    }

    public List<GuildMember> Artisans()
    {
        List<GuildMember> artisans = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Artisan)
            {
                artisans.Add(guildMember);
            }
        }
        return artisans;
    }

    public List<GuildMember> Peasants()
    {
        List<GuildMember> peasants = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.Vocation is Peasant)
            {
                peasants.Add(guildMember);
            }
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
        GuildMembers.Remove(_guildMember);
    }

    public void SortGuildMembersByLevel()
    {
        List<GuildMember> sorted = GuildMembers.OrderByDescending(gm => gm.Level).ToList();
        GuildMembers = sorted;
    }

    public void EnableArtisans()
    {
        ArtisansEnabled = true;
    }

    private void RecoverHitpoints()
    {
        TimeSpan difference = DateTime.Now - RecoveryStartTime;
        recoveryTime = (float)difference.TotalSeconds;
        recoveryQueue = Mathf.FloorToInt(recoveryTime / recoveryTimer);
        for (int i = 0; i < recoveryQueue; i++)
        {
            foreach (GuildMember guildMember in GuildMembers)
            {
                if (guildMember.Hitpoints != guildMember.MaxHitpoints && guildMember.IsAvailable)
                {
                    guildMember.AdjustHitpoints(hitpointRecovery);
                }
            }
        }
        if (DateTime.Now > RecoveryStartTime.AddSeconds(recoveryTimer))
        {
            RecoveryStartTime = DateTime.Now;
        }
    }

    private void PassiveRecruitment()
    {
        TimeSpan difference = DateTime.Now - RecruitStartTime;
        recruitTime = (float)difference.TotalSeconds;
        recruitQueue = Mathf.FloorToInt(recruitTime / recruitTimer);
        for (int i = 0; i < recruitQueue; i++)
        {
            CheckForRecruit();
        }
        if (DateTime.Now > RecruitStartTime.AddSeconds(recruitTimer))
        {
            RecruitStartTime = DateTime.Now;
        }
    }

    private void CheckForRecruit()
    {
        float odds = ((PopulationCap - GuildMembers.Count) * guildhall.Renown) / (Levelling.RenownLevel[guildhall.RenownLevel] * PopulationCap) * 0.5f;
        float roll = UnityEngine.Random.Range(0.01f, 1.0f);
        if (roll <= odds) CreateGuildMember();
    }
}
