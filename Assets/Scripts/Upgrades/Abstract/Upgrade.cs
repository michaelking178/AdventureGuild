using System.Collections;
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

    private LevelManager levelManager;
    protected Guildhall guildhall;
    protected PopulationManager populationManager;

    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
        StartCoroutine(DelayedCheckForUpgrade());
    }

    private void Update()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>().CurrentMenu.name == "Menu_UpgradeGuildhall")
        {
            CheckForUpgrade();
        }
    }

    protected virtual void CheckForUpgrade(){}

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

    private IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade();
        yield return null;
    }
}
