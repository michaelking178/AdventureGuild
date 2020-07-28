using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        GuildMember hero = GameObject.Find("Hero").GetComponent<GuildMember>();
        GuildMemberData heroData = new GuildMemberData(hero.person, hero.GetVocation(), hero.GetAvatar(), hero.GetHealth(), hero.GetExp(), hero.GetLevel(), hero.IsAvailable());

        List<Quest> questPool = Object.FindObjectOfType<QuestManager>().GetQuestPool();

        Guildhall guildhall = Object.FindObjectOfType<Guildhall>();
        GuildhallData guildhallData = new GuildhallData(guildhall.GetGold(), guildhall.GetIron(), guildhall.GetWood(), guildhall.GetWeapons());

        List<GuildMember> guildMembers = Object.FindObjectOfType<PopulationManager>().GuildMembers;
        List<GuildMemberData> guildMemberDatas = new List<GuildMemberData>();
        foreach (GuildMember guildMember in guildMembers)
        {
            GuildMemberData guildMemberData = new GuildMemberData(guildMember.person, guildMember.GetVocation(), guildMember.GetAvatar(), guildMember.GetHealth(), guildMember.GetExp(), guildMember.GetLevel(), guildMember.IsAvailable());
            guildMemberDatas.Add(guildMemberData);
        }

        SaveData saveData = new SaveData(heroData, guildhallData, guildMemberDatas);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static void LoadGame()
    {
        string path = Application.persistentDataPath + "/saveData.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData saveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            saveData.Load();
        }
        else
        {
            Debug.LogError("Save File not found!");
            return;
        }
    }
}
