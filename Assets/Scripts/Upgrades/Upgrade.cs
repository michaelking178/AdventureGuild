using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public bool IsPurchased { get; set; } = false;
    public int GoldCost;
    public int IronCost;
    public int WoodCost;
    public int ArtisanCost;

    protected Guildhall guildhall;
    protected PopulationManager populationManager;

    public void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
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
        if (guildhall.Gold < GoldCost || guildhall.Wood < WoodCost || guildhall.Iron < IronCost || populationManager.Artisans().Count < ArtisanCost)
        {
            return false;
        }
        return true;
    }
}
