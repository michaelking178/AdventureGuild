using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{

    [SerializeField]
    private TextAsset peopleJson;

    [SerializeField]
    private People peopleList;

    [SerializeField]
    private GuildMember guildMemberPrefab;

    public List<GuildMember> GuildMembers = new List<GuildMember>();

    private void Start()
    {
        peopleList = JsonUtility.FromJson<People>(peopleJson.text);
    }

    public void CreateGuildMember()
    {
        Person randomPerson = peopleList.people[Random.Range(0, peopleList.people.Length)];
        GuildMember newMember = Instantiate(guildMemberPrefab, transform);
        newMember.Init(randomPerson);
        GuildMembers.Add(newMember);
    }

    public List<GuildMember> Adventurers()
    {
        List<GuildMember> adventurers = new List<GuildMember>();
        foreach(GuildMember guildMember in GuildMembers)
        {
            if (guildMember.GetVocation() is Adventurer)
            {
                adventurers.Add(guildMember);
            }
        }
        return adventurers;
    }

    public List<GuildMember> GetAvailableAdventurers()
    {
        List<GuildMember> adventurers = new List<GuildMember>();
        foreach (GuildMember guildMember in GuildMembers)
        {
            if (guildMember.GetVocation() is Adventurer && guildMember.IsAvailable())
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
            if (guildMember.GetVocation() is Artisan)
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
            if (guildMember.GetVocation() is Peasant)
            {
                peasants.Add(guildMember);
            }
        }
        return peasants;
    }
}
