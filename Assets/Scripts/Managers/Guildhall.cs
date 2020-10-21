using System;
using UnityEngine;
using UnityEngineInternal;

[Serializable]
public class Guildhall : MonoBehaviour
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Renown { get; set; }
    public float renownThreshold = 10.0f;

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
        if (Renown >= renownThreshold)
        {
            IncrementRenown();
            populationManager.CreateGuildMember();
            questManager.PopulateQuestPool(UnityEngine.Random.Range(3, 6));
        }
    }

    private void IncrementRenown()
    {
        if (renownThreshold < 50)
        {
            renownThreshold *= 2f;
        }
        else if (renownThreshold < 250)
        {
            renownThreshold *= 1.5f;
        }
        else if (renownThreshold < 480)
        {
            renownThreshold = Mathf.RoundToInt(renownThreshold * 1.333333f);
        }
        else if (renownThreshold < 600)
        {
            renownThreshold *= 1.25f;
        }
        else if (renownThreshold < 1200)
        {
            renownThreshold += 150;
        }
        else
        {
            renownThreshold += 200;
        }
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
