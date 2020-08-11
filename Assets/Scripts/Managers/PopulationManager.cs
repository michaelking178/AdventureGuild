using System.Collections.Generic;
using UnityEngine;

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

    private int recoveryQueue;
    private float recoveryTime;
    private float recoveryCheckpoint = 30.0f;
    public System.DateTime recoveryStartTime;

    private void Start()
    {
        maleNames = JsonUtility.FromJson<Names>(maleNamesJson.text);
        femaleNames = JsonUtility.FromJson<Names>(femaleNamesJson.text);
        lastNames = JsonUtility.FromJson<Names>(lastNamesJson.text);
        if (recoveryStartTime == System.DateTime.MinValue)
        {
            recoveryStartTime = System.DateTime.Now;
        }
    }

    private void FixedUpdate()
    {
        RecoverHitpoints();
    }

    public void CreateGuildMember()
    {
        string firstName;
        string lastName = lastNames.prefixes[Random.Range(0, lastNames.prefixes.Length)] + lastNames.suffixes[Random.Range(0, lastNames.suffixes.Length)];
        Sprite avatar;
        int gender = Random.Range(0, 2);
        if (gender == 0)
        {
            firstName = maleNames.prefixes[Random.Range(0, maleNames.prefixes.Length)] + maleNames.suffixes[Random.Range(0, maleNames.suffixes.Length)];
            avatar = maleAvatars[Random.Range(0, maleAvatars.Count)];
        }
        else
        {
            firstName = femaleNames.prefixes[Random.Range(0, femaleNames.prefixes.Length)] + femaleNames.suffixes[Random.Range(0, femaleNames.suffixes.Length)];
            avatar = femaleAvatars[Random.Range(0, femaleAvatars.Count)];
        }
        Person newPerson = new Person(gender, firstName, lastName);
        GuildMember newMember = Instantiate(guildMemberPrefab, transform);
        newMember.Init(newPerson);
        newMember.Avatar = avatar;
        GuildMembers.Add(newMember);
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
        newMember.Level = guildMemberData.level;
        newMember.Vocation = guildMemberData.vocation;
        newMember.IsAvailable = guildMemberData.isAvailable;
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

    private void RecoverHitpoints()
    {
        System.TimeSpan difference = System.DateTime.Now - recoveryStartTime;
        recoveryTime = (float)difference.TotalSeconds;
        recoveryQueue = Mathf.FloorToInt(recoveryTime / recoveryCheckpoint);
        for (int i = 0; i < recoveryQueue; i++)
        {
            foreach (GuildMember guildMember in GuildMembers)
            {
                if (guildMember.Hitpoints != guildMember.MaxHitpoints && guildMember.IsAvailable)
                {
                    guildMember.AdjustHitpoints(5);
                }
            }
        }
        if (System.DateTime.Now > recoveryStartTime.AddSeconds(recoveryCheckpoint))
        {
            recoveryStartTime = System.DateTime.Now;
        }
    }
}
