using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GuildhallData
{
    public int gold;
    public int iron;
    public int wood;
    public int weapons;

    public GuildhallData(int _gold, int _iron, int _wood, int _weapons)
    {
        gold = _gold;
        iron = _iron;
        wood = _wood;
        weapons = _weapons;
    }
}
