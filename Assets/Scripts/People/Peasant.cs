using System;
using UnityEngine;

[Serializable]
public class Peasant : Vocation
{
    public enum IncomeType { Gold, Wood, Iron };
    public int Income { get; set; }
    public IncomeType IncomeResource { get; set; }

    public Peasant()
    {
        title = "Peasant";
        MaxLevel = 10;
        IncomeResource = (IncomeType)UnityEngine.Random.Range(0, 3);
        SetIncome();
    }

    private void SetIncome()
    {
        if (IncomeResource == IncomeType.Gold)
        {
            Income = UnityEngine.Random.Range(10, 30);
        }
        else if (IncomeResource == IncomeType.Wood)
        {
            Income = UnityEngine.Random.Range(5, 15);
        }
        else if (IncomeResource == IncomeType.Iron)
        {
            Income = UnityEngine.Random.Range(3, 10);
        }
        GameObject.FindObjectOfType<Guildhall>().AdjustIncome(IncomeResource, Income);
    }
}
