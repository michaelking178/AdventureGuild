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

        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        GuildMemberData heroData = new GuildMemberData(hero);

        Guildhall guildhall = Object.FindObjectOfType<Guildhall>();
        GuildhallData guildhallData = new GuildhallData(guildhall);

        List<GuildMemberData> guildMemberDatas = new List<GuildMemberData>();
        foreach (GuildMember guildMember in Object.FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.GetInstanceID() != hero.GetInstanceID())
            {
                GuildMemberData guildMemberData = new GuildMemberData(guildMember);
                guildMemberDatas.Add(guildMemberData);
            }
        }

        List<QuestData> questDataPool = new List<QuestData>();
        foreach(Quest quest in Object.FindObjectOfType<QuestManager>().GetQuestPool())
        {
            QuestData questData = new QuestData(quest);
            questDataPool.Add(questData);
        }

        SaveData saveData = new SaveData(heroData, guildhallData, guildMemberDatas, questDataPool);
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
