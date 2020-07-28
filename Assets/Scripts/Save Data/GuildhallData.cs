using System;

[Serializable]
public class GuildhallData
{
    public int gold;
    public int iron;
    public int wood;
    public int weapons;

    public GuildhallData(Guildhall guildhall)
    {
        gold = guildhall.GetGold();
        iron = guildhall.GetIron();
        wood = guildhall.GetWood();
        weapons = guildhall.GetWeapons();
    }
}
