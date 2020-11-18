using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public Upgrade ConstructionJob;
    public bool UnderConstruction = false;
    public float TimeElapsed;
    public List<GuildMember> Artisans { get; set; }
    public DateTime StartTime;

    public string ConstructionName = "";

    private void Start()
    {
        Artisans = new List<GuildMember>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<LevelManager>().CurrentLevel() == "Main")
        {
            Upgrade[] upgrades = FindObjectsOfType<Upgrade>();
            foreach (Upgrade upgrade in upgrades)
            {
                if (upgrade.Name == ConstructionName)
                {
                    ConstructionJob = upgrade;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (ConstructionJob != null && UnderConstruction)
        {
            TimeElapsed = (float)(DateTime.Now - StartTime).TotalSeconds;

            if (TimeElapsed >= ConstructionJob.constructionTime)
            {
                CompleteConstruction();
            }
        }
    }

    public void SetConstructionJob(Upgrade upgrade)
    {
        ConstructionJob = upgrade;
        ConstructionName = upgrade.Name;
    }

    public void AddArtisan(GuildMember artisan)
    {
        Artisans.Add(artisan);
    }

    public void RemoveArtisan(GuildMember artisan)
    {
        Artisans.Remove(artisan);
    }

    public void BeginConstruction()
    {
        UnderConstruction = true;
        // ConstructionJob.PayForUpgrade();
        StartTime = DateTime.Now;
    }

    public void CompleteConstruction()
    {
        UnderConstruction = false;
        ConstructionJob.Apply();
        FindObjectOfType<NotificationManager>().CreateNotification($"{ConstructionJob.Name} is finished construction!", Notification.Spirit.Good);
        ConstructionJob = null;
    }

    public void ClearConstruction()
    {
        if (!UnderConstruction)
        {
            ConstructionJob = null;
        }
    }

    public int SelectedArtisansProficiency()
    {
        int pro = 0;
        foreach (GuildMember artisan in Artisans)
        {
            pro += artisan.Level;
        }
        return pro;
    }
}
