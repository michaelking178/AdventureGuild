using System;

[Serializable]
public class GuildhallData
{
    public int gold;
    public int iron;
    public int wood;
    public int weapons;
    public int renown;

    public GuildhallData(Guildhall guildhall)
    {
        gold = guildhall.Gold;
        iron = guildhall.Iron;
        wood = guildhall.Wood;
        weapons = guildhall.Weapons;
        renown = guildhall.Renown;
    }
}
