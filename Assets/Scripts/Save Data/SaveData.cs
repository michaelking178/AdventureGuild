using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    private GuildhallData guildhallData;
    private List<GuildMemberData> guildMemberDatas;
    private GuildMemberData hero;

    public SaveData(GuildMemberData _hero, GuildhallData _guildhallData, List<GuildMemberData> _guildMemberDatas)
    {
        hero = _hero;
        guildhallData = _guildhallData;
        guildMemberDatas = _guildMemberDatas;
    }

    public void Load()
    {
        // TODO: load the Quest Pool. Need to create a serializable questData list.

        GuildMember heroGM = GameObject.Find("Hero").GetComponent<GuildMember>();
        heroGM.person = hero.person;
        heroGM.SetVocation(hero.vocation);
        Sprite _heroAvatar = Helpers.ConvertImageToSprite(hero.avatarPath);
        heroGM.SetAvatar(_heroAvatar);
        heroGM.SetHealth(hero.health);
        heroGM.SetExp(hero.experience);
        heroGM.SetLevel(hero.level);
        heroGM.IsAvailable(hero.isAvailable);

        Guildhall guildhall = GameObject.Find("Guildhall").GetComponent<Guildhall>();
        guildhall.SetGold(guildhallData.gold);
        guildhall.SetIron(guildhallData.iron);
        guildhall.SetWood(guildhallData.wood);
        guildhall.SetWeapons(guildhallData.weapons);

        List<GuildMember> guildList = new List<GuildMember>();
        foreach (GuildMemberData guildMemberData in guildMemberDatas)
        {
            GuildMember newMember = new GuildMember();
            newMember.person = guildMemberData.person;
            Sprite _avatar = Helpers.ConvertImageToSprite(guildMemberData.avatarPath);
            newMember.SetAvatar(_avatar);
            newMember.SetExp(guildMemberData.experience);
            newMember.SetHealth(guildMemberData.health);
            newMember.SetLevel(guildMemberData.level);
            newMember.SetVocation(guildMemberData.vocation);
            newMember.IsAvailable(guildMemberData.isAvailable);
        }
        GameObject.Find("Population Manager").GetComponent<PopulationManager>().GuildMembers = guildList;
    }
}
