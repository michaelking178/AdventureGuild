using System;
using UnityEngine;

[Serializable]
public class Peasant : Vocation
{
    public enum IncomeType { Gold, Wood, Iron };
    public int Income { get; set; }
    public int BaseIncome { get; private set; }
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
            BaseIncome = UnityEngine.Random.Range(10, 30);
            Income = BaseIncome;
        }
        else if (IncomeResource == IncomeType.Wood)
        {
            BaseIncome = UnityEngine.Random.Range(5, 15);
            Income = BaseIncome;
        }
        else if (IncomeResource == IncomeType.Iron)
        {
            BaseIncome = UnityEngine.Random.Range(3, 10);
            Income = BaseIncome;
        }
        GameObject.FindObjectOfType<Guildhall>().AdjustIncome(IncomeResource, Income);
    }
}
