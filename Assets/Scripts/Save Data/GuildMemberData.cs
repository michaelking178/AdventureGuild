using System;

[Serializable]
public class GuildMemberData
{
    public int id;
    public Person person;
    public Vocation vocation;
    public Adventurer adventurer;
    public string avatarSpriteName = "";
    public int maxHitpoints;
    public int hitpoints;
    public int experience;
    public int level;
    public bool isAvailable;
    public string bio = "";
    public string quip = "";

    public GuildMemberData(GuildMember guildMember)
    {
        id = guildMember.Id;
        person = guildMember.person;
        if (guildMember.Vocation is Adventurer)
        {
            adventurer = (Adventurer)guildMember.Vocation;
        }
        else
        {
            vocation = guildMember.Vocation;
        }
        if (guildMember.Avatar != null)
        {
            avatarSpriteName = guildMember.Avatar.name;
        }
        maxHitpoints = guildMember.MaxHitpoints;
        hitpoints = guildMember.Hitpoints;
        experience = guildMember.Experience;
        level = guildMember.Level;
        isAvailable = guildMember.IsAvailable;
        bio = guildMember.Bio;
        quip = guildMember.Quip;
    }
}
