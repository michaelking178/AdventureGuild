using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public TierUpgrade ConstructionJob;
    public bool UnderConstruction = false;
    public float TimeElapsed;
    public List<GuildMember> Artisans = new List<GuildMember>();
    public DateTime StartTime;
    public string ConstructionName = "";

    private LevelManager levelManager;
    private TierUpgradeObject constructionTier;

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

            if (TimeElapsed >= constructionTier.constructionTime)
            {
                UnderConstruction = false;
                StartCoroutine(CompleteConstruction());
            }
        }
    }

    public void SetConstructionJob(TierUpgrade upgrade)
    {
        ConstructionJob = upgrade;
        constructionTier = ConstructionJob.UpgradeTiers[ConstructionJob.NextTier()];
        ConstructionName = constructionTier.Name;
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
            int artisanExpShare = Mathf.RoundToInt(constructionTier.Experience / Artisans.Count);
            foreach (GuildMember artisan in Artisans)
            {
                artisan.AddExp(artisanExpShare);
                artisan.IsAvailable = true;
            }
        }

        FindObjectOfType<NotificationManager>().CreateNotification($"Artisans have completed the {constructionTier.Name} construction job!", Notification.Spirit.Good);
        ClearConstruction();
    }

    public void ClearConstruction()
    {
        if (!UnderConstruction)
        {
            TimeElapsed = 0;
            ConstructionName = "";
            ConstructionJob = null;
            constructionTier = null;
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

    public TierUpgrade GetUpgrade(string upgradeName)
    {
        foreach(GameObject upgradeObj in gameObject.GetChildren())
        {
            if (upgradeObj.GetComponent<TierUpgrade>().name == upgradeName)
            {
                return upgradeObj.GetComponent<TierUpgrade>();
            }
        }
        Debug.Log($"CONSTRUCTIONMANAGER.CS: Cannot find Upgrade with name {upgradeName}");
        return null;
    }
}
