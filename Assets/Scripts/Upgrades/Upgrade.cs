using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public bool IsPurchased { get; set; } = false;
    public int GoldCost;
    public int IronCost;
    public int WoodCost;
    public int ArtisanCost;

    protected Guildhall guildhall;

    public void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
    }

    public virtual void Apply()
    {
        IsPurchased = true;
        guildhall.AdjustGold(-GoldCost);
        guildhall.AdjustWood(-WoodCost);
        guildhall.AdjustIron(-IronCost);
    }

    public bool CanAfford()
    {
        if (guildhall.Gold < GoldCost || guildhall.Wood < WoodCost || guildhall.Iron < IronCost)
        {
            return false;
        }
        return true;
    }
}
