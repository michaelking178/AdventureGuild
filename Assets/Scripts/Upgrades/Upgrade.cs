using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public string Name;
    public string Description;
    public bool IsPurchased { get; set; } = false;
    public int GoldCost;
    public int IronCost;
    public int WoodCost;
    public int ArtisanCost;
    public float constructionTime;
    public int Experience;

    protected Guildhall guildhall;
    protected PopulationManager populationManager;

    public void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public virtual void PayForUpgrade()
    {
        guildhall.AdjustGold(-GoldCost);
        guildhall.AdjustWood(-WoodCost);
        guildhall.AdjustIron(-IronCost);
    }

    public virtual void Apply()
    {
        IsPurchased = true;
    }

    public bool CanAfford()
    {
        if (guildhall.Gold < GoldCost || guildhall.Wood < WoodCost || guildhall.Iron < IronCost || guildhall.ArtisanProficiency < ArtisanCost)
        {
            return false;
        }
        return true;
    }
}
