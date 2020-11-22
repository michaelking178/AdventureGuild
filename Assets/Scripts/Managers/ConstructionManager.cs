using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public Upgrade ConstructionJob;
    public bool UnderConstruction = false;
    public float TimeElapsed;
    public List<GuildMember> Artisans;
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
        StartTime = DateTime.Now;
        foreach(GuildMember artisan in Artisans)
        {
            artisan.IsAvailable = false;
        }
    }

    public IEnumerator CompleteConstruction()
    {
        UnderConstruction = false;
        yield return new WaitForSeconds(0.5f);
        ConstructionJob.Apply();

        if (Artisans.Count != 0)
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
        foreach(GameObject upgradeObj in Helpers.GetChildren(gameObject))
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
