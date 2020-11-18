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
            if (guildMember.Id != hero.Id)
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

        List<QuestData> questDataArchive = new List<QuestData>();
        foreach (Quest quest in Object.FindObjectOfType<QuestManager>().GetQuestArchive())
        {
            QuestData questData = new QuestData(quest);
            questDataArchive.Add(questData);
        }

        List<QuestTimerData> questTimerDatas = new List<QuestTimerData>();
        foreach (GameObject questTimerObj in Helpers.GetChildren(Object.FindObjectOfType<QuestManager>().gameObject))
        {
            QuestTimer questTimer = questTimerObj.GetComponent<QuestTimer>();
            if (questTimer != null)
            {
                QuestTimerData questTimerData = new QuestTimerData(questTimer);
                questTimerDatas.Add(questTimerData);
            }
        }

        var soundMan = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        var musicMan = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        SettingsData settingsData = new SettingsData(soundMan.volume, musicMan.volume, GameObject.Find("Population Manager").GetComponent<PopulationManager>().DebugBoostEnabled);

        PopulationManagerData populationManagerData = new PopulationManagerData(Object.FindObjectOfType<PopulationManager>());

        QuestManagerData questManagerData = new QuestManagerData(Object.FindObjectOfType<QuestManager>());

        ConstructionManagerData constructionManagerData = new ConstructionManagerData(Object.FindObjectOfType<ConstructionManager>());

        SaveData saveData = new SaveData(
            heroData, 
            guildhallData, 
            guildMemberDatas, 
            questDataPool, 
            questDataArchive, 
            questTimerDatas, 
            settingsData, 
            populationManagerData, 
            questManagerData,
            constructionManagerData
            );

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

    public static void DeleteGame()
    {
        string path = Application.persistentDataPath + "/saveData.bin";
        if (!File.Exists(path))
        {
            return;
        }
        File.Delete(path);
    }

    public static bool SaveFileExists()
    {
        string path = Application.persistentDataPath + "/saveData.bin";
        return File.Exists(path);
    }

    public static string GetSaveVersion()
    {
        string path = Application.persistentDataPath + "/saveData.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData saveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return saveData.ApplicationVersion;
        }
        else
        {
            Debug.LogError("Save File not found!");
            return null;
        }
    }
}
