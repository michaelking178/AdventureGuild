using System;
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
    public int ArtisanProficiency;

    public int MaxGold { get; set; } = 2500;
    public int MaxIron { get; set; } = 1000;
    public int MaxWood { get; set; } = 1000;

    public int GoldIncome { get; set; } = 0;
    public int IronIncome { get; set; } = 0;
    public int WoodIncome { get; set; } = 0;

    public int MaxGoldIncome { get; set; } = 250;
    public int MaxIronIncome { get; set; } = 100;
    public int MaxWoodIncome { get; set; } = 100;
    public DateTime StartTime { get; set; }

    private LevelManager levelManager;
    private PopulationManager populationManager;
    private QuestManager questManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        populationManager = FindObjectOfType<PopulationManager>();
        questManager = FindObjectOfType<QuestManager>();
        if (StartTime == DateTime.MinValue)
        {
            StartTime = DateTime.Now;
        }
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        CalculateArtisanProficiency();
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
        else if (Gold > MaxGold)
        {
            Gold = MaxGold;
        }
    }

    public void AdjustIron(int change)
    {
        Iron += change;
        if (Iron < 0)
        {
            Iron = 0;
        }
        else if (Iron > MaxIron)
        {
            Iron = MaxIron;
        }
    }

    public void AdjustWood(int change)
    {
        Wood += change;
        if (Wood < 0)
        {
            Wood = 0;
        }
        else if (Wood > MaxWood)
        {
            Wood = MaxWood;
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
