using System;

[Serializable]
public class GuildhallData
{
    public int gold;
    public int iron;
    public int wood;
    public int weapons;
    public int renown;
    public int renownLevel;
    public int goldIncome;
    public int ironIncome;
    public int woodIncome;
    public DateTime startTime;

    public GuildhallData(Guildhall guildhall)
    {
        gold = guildhall.Gold;
        iron = guildhall.Iron;
        wood = guildhall.Wood;
        weapons = 0;
        renown = guildhall.Renown;
        renownLevel = guildhall.RenownLevel;
        goldIncome = guildhall.GoldIncome;
        ironIncome = guildhall.IronIncome;
        woodIncome = guildhall.WoodIncome;
        startTime = guildhall.StartTime;
    }
}
