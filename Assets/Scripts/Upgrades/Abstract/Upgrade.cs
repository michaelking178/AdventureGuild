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

    protected LevelManager levelManager;
    protected Guildhall guildhall;
    protected PopulationManager populationManager;

    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void PayForUpgrade()
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
