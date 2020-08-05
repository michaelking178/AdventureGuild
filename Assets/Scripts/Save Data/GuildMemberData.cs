using System;

[Serializable]
public class GuildMemberData
{
    public int id;
    public Person person;
    public Vocation vocation;
    public string avatarSpriteName = "";
    public int health;
    public int experience;
    public int level;
    public bool isAvailable;

    public GuildMemberData(GuildMember guildMember)
    {
        id = guildMember.GetInstanceID();
        person = guildMember.person;
        vocation = guildMember.GetVocation();
        if (avatarSpriteName != "")
        {
            avatarSpriteName = guildMember.GetAvatar().name;
        }
        health = guildMember.GetHealth();
        experience = guildMember.GetExp();
        level = guildMember.GetLevel();
        isAvailable = guildMember.IsAvailable();
    }
}
