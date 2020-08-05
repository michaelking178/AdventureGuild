using System;
using UnityEngine;

[Serializable]
public class Guildhall : MonoBehaviour
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Weapons { get; set; }
    public int Renown { get; set; }

    public void AdjustGold(int change)
    {
        Gold += change;
    }

    public void AdjustIron(int change)
    {
        Iron += change;
    }

    public void AdjustWood(int change)
    {
        Wood += change;
    }

    public void AdjustWeapons(int change)
    {
        Weapons += change;
    }

    public void AdjustRenown(int change)
    {
        Renown += change;
    }
}
