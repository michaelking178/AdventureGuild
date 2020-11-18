﻿using System;
using UnityEngine;

[Serializable]
public class Guildhall : MonoBehaviour
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Renown { get; set; }
    public int RenownLevel { get; set; } = 1;
    public int RenownThreshold { get { return Levelling.RenownLevel[RenownLevel]; } }
    public int ArtisanProficiency { get; set; }

    public int GoldIncome { get; set; } = 0;
    public int IronIncome { get; set; } = 0;
    public int WoodIncome { get; set; } = 0;
    public DateTime StartTime { get; set; }

    private PopulationManager populationManager;
    private QuestManager questManager;

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        questManager = FindObjectOfType<QuestManager>();
        if (StartTime == DateTime.MinValue)
        {
            StartTime = DateTime.Now;
        }
    }

    private void FixedUpdate()
    {
        if (Renown >= Levelling.RenownLevel[RenownLevel])
        {
            RenownLevel++;
            populationManager.CreateGuildMember();
            questManager.PopulateQuestPool(UnityEngine.Random.Range(3, 6));
        }
    }

    public void CalculateArtisanProficiency()
    {
        int proficiency = 0;
        foreach (GuildMember guildmember in populationManager.Artisans())
        {
            proficiency += guildmember.Level;
        }
        ArtisanProficiency = proficiency;
    }

    public void AdjustGold(int change)
    {
        Gold += change;
        if (Gold < 0)
        {
            Gold = 0;
        }
    }

    public void AdjustIron(int change)
    {
        Iron += change;
        if (Iron < 0)
        {
            Iron = 0;
        }
    }

    public void AdjustWood(int change)
    {
        Wood += change;
        if (Wood < 0)
        {
            Wood = 0;
        }
    }

    public void AdjustRenown(int change)
    {
        Renown += change;
        if (Renown < 0)
        {
            Renown = 0;
        }
    }

    public void AdjustIncome(Peasant.IncomeType incomeType, int _income)
    {
        switch (incomeType)
        {
            case Peasant.IncomeType.Gold:
                GoldIncome += _income;
                break;
            case Peasant.IncomeType.Wood:
                WoodIncome += _income;
                break;
            case Peasant.IncomeType.Iron:
                IronIncome += _income;
                break;
            default:
                break;
        }
    }
}
