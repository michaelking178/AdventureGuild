using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public Upgrade ConstructionJob;
    public bool UnderConstruction = false;
    public float TimeElapsed;
    public List<GuildMember> Artisans = new List<GuildMember>();
    public DateTime StartTime;
    public string ConstructionName = "";

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (ConstructionJob != null && UnderConstruction)
        {
            TimeElapsed = (float)(DateTime.Now - StartTime).TotalSeconds;

            if (TimeElapsed >= ConstructionJob.constructionTime)
            {
                UnderConstruction = false;
                StartCoroutine(CompleteConstruction());
            }
        }
    }

    public void SetConstructionJob(Upgrade upgrade)
    {
        ConstructionJob = upgrade;
        ConstructionName = upgrade.name;
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
        StartTime = DateTime.Now.AddSeconds(-0.25);
        foreach(GuildMember artisan in Artisans)
        {
            artisan.IsAvailable = false;
        }
    }

    public IEnumerator CompleteConstruction()
    {
        yield return new WaitForSeconds(0.5f);
        ConstructionJob.Apply();

        if (Artisans.Count > 0)
        {
            int artisanExpShare = Mathf.RoundToInt(ConstructionJob.Experience / Artisans.Count);
            foreach (GuildMember artisan in Artisans)
            {
                artisan.AddExp(artisanExpShare);
                artisan.IsAvailable = true;
            }
        }

        FindObjectOfType<NotificationManager>().CreateNotification($"Artisans have completed the {ConstructionJob.Name} construction job!", Notification.Spirit.Good);
        ClearConstruction();
    }

    public void ClearConstruction()
    {
        if (!UnderConstruction)
        {
            TimeElapsed = 0;
            ConstructionName = "";
            ConstructionJob = null;
            Artisans.Clear();
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
        foreach(GameObject upgradeObj in gameObject.GetChildren())
        {
            if (upgradeObj.GetComponent<Upgrade>().name == upgradeName)
            {
                return upgradeObj.GetComponent<Upgrade>();
            }
        }
        Debug.Log($"CONSTRUCTIONMANAGER.CS: Cannot find Upgrade with name {upgradeName}");
        return null;
    }
}
