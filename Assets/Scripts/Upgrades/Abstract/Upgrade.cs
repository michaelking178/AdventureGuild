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

    protected LevelManager levelManager;
    protected Guildhall guildhall;
    protected PopulationManager populationManager;
    protected QuestManager questManager;

    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
        questManager = FindObjectOfType<QuestManager>();
        StartCoroutine(DelayedCheckForUpgrade());
    }

    protected virtual IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade();
        yield return null;
    }

    protected virtual void CheckForUpgrade() 
    {
        Debug.Log($"{gameObject.name} has not defined CheckForUpgrade() and is using the empty base class method!");
    }

    public void PayForUpgrade()
    {
        Guildhall guildhall = FindObjectOfType<Guildhall>();
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
