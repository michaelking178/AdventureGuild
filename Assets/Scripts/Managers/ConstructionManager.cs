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
        ConstructionJob.PayForUpgrade();
        StartTime = DateTime.Now;
        foreach(GuildMember artisan in Artisans)
        {
            artisan.IsAvailable = false;
        }
    }

    public void CompleteConstruction()
    {
        UnderConstruction = false;
        ConstructionJob.Apply();

        int artisanExpShare = Mathf.RoundToInt(ConstructionJob.Experience / Artisans.Count);
        foreach (GuildMember artisan in Artisans)
        {
            artisan.AddExp(artisanExpShare);
            artisan.IsAvailable = true;
        }
        Artisans.Clear();

        FindObjectOfType<NotificationManager>().CreateNotification($"Artisans have completed the {ConstructionJob.Name} construction job!", Notification.Spirit.Good);
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

    public Upgrade GetUpgrade(string upgradeName)
    {
        foreach(GameObject upgradeObj in Helpers.GetChildren(gameObject))
        {
            if (upgradeObj.GetComponent<Upgrade>().name == upgradeName)
            {
                return upgradeObj.GetComponent<Upgrade>();
            }
        }
        Debug.Log($"Cannot find Upgrade with name {upgradeName}");
        return null;
    }
}
