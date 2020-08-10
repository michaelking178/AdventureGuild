using System;

[Serializable]
public class GuildMemberData
{
    public int id;
    public Person person;
    public Vocation vocation;
    public string avatarSpriteName = "";
    public int maxHitpoints;
    public int hitpoints;
    public int experience;
    public int level;
    public bool isAvailable;

    public GuildMemberData(GuildMember guildMember)
    {
        id = guildMember.Id;
        person = guildMember.person;
        vocation = guildMember.Vocation;
        if (guildMember.Avatar != null)
        {
            avatarSpriteName = guildMember.Avatar.name;
        }
        maxHitpoints = guildMember.MaxHitpoints;
        hitpoints = guildMember.Hitpoints;
        experience = guildMember.Experience;
        level = guildMember.Level;
        isAvailable = guildMember.IsAvailable;
    }
}
