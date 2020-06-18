using UnityEngine;

public class Guildhall : MonoBehaviour
{
    [SerializeField]
    private int gold;

    [SerializeField]
    private int iron;

    [SerializeField]
    private int wood;

    [SerializeField]
    private int weapons;

    public int GetGold()
    {
        return gold;
    }

    public void ChangeGoldBy(int change)
    {
        gold += change;
    }

    public int GetIron()
    {
        return iron;
    }

    public void ChangeIronBy(int change)
    {
        iron += change;
    }

    public int GetWood()
    {
        return wood;
    }

    public void ChangeWoodBy(int change)
    {
        wood += change;
    }

    public int GetWeapons()
    {
        return weapons;
    }

    public void ChangeWeaponsBy(int change)
    {
        weapons += change;
    }
}
