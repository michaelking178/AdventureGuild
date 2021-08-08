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
    public int LegendLevel { get; private set; } = 0;

    public int MaxGold { get; set; } = 25000;
    public int MaxIron { get; set; } = 10000;
    public int MaxWood { get; set; } = 10000;

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

    public delegate void OnGuildhallChallengeAction(int value);
    public static event OnGuildhallChallengeAction OnGoldReward;
    public static event OnGuildhallChallengeAction OnWoodReward;
    public static event OnGuildhallChallengeAction OnIronReward;
    public static event OnGuildhallChallengeAction OnRenownReward;

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
        
        if (populationManager != null)
        {
            CalculateArtisanProficiency();
            if (Renown >= Levelling.RenownLevel[RenownLevel])
            {
                RenownLevel++;
                populationManager.CreateGuildMember();
                questManager.PopulateQuestPool(UnityEngine.Random.Range(3, 6));
            }
        }
    }

    public void CalculateArtisanProficiency()
    {
        if (populationManager != null)
        {
            int proficiency = 0;
            for (int i = 0; i < populationManager.Artisans().Count; i++)
            {
                proficiency += populationManager.Artisans()[i].Level;
            }
            ArtisanProficiency = proficiency;
        }
    }

    public void AdjustGold(int change)
    {
        Gold += change;
        if (Gold < 0)
            Gold = 0;
        else if (Gold > MaxGold)
            Gold = MaxGold;

        // Challenge reward event
        if (change > 0)
            OnGoldReward?.Invoke(change);
    }

    public void AdjustIron(int change)
    {
        Iron += change;
        if (Iron < 0)
            Iron = 0;
        else if (Iron > MaxIron)
            Iron = MaxIron;

        // Challenge reward event
        if (change > 0)
            OnIronReward?.Invoke(change);
    }

    public void AdjustWood(int change)
    {
        Wood += change;
        if (Wood < 0)
            Wood = 0;
        else if (Wood > MaxWood)
            Wood = MaxWood;

        // Challenge reward event
        if (change > 0)
            OnWoodReward?.Invoke(change);
    }

    public void AdjustRenown(int change)
    {
        Renown += change;
        if (Renown < 0)
            Renown = 0;

        // Challenge reward event
        if (change > 0)
            OnRenownReward?.Invoke(change);
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
