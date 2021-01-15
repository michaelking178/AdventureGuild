using System;

[Serializable]
public class GuildhallData
{
    public int gold;
    public int iron;
    public int wood;
    public int maxGold;
    public int maxIron;
    public int maxWood;
    public int weapons;
    public int renown;
    public int renownLevel;
    public int goldIncome;
    public int ironIncome;
    public int woodIncome;
    public int maxGoldIncome;
    public int maxIronIncome;
    public int maxWoodIncome;
    public DateTime startTime;

    public GuildhallData(Guildhall guildhall)
    {
        gold = guildhall.Gold;
        iron = guildhall.Iron;
        wood = guildhall.Wood;
        maxGold = guildhall.MaxGold;
        maxIron = guildhall.MaxIron;
        maxWood = guildhall.MaxWood;
        weapons = 0;
        renown = guildhall.Renown;
        renownLevel = guildhall.RenownLevel;
        goldIncome = guildhall.GoldIncome;
        ironIncome = guildhall.IronIncome;
        woodIncome = guildhall.WoodIncome;
        maxGoldIncome = guildhall.MaxGoldIncome;
        maxIronIncome = guildhall.MaxIronIncome;
        maxWoodIncome = guildhall.MaxWoodIncome;
        startTime = guildhall.StartTime;
    }
}
