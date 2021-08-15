using UnityEngine;

public class TierUpgrade : MonoBehaviour
{
    public TierUpgradeObject[] UpgradeTiers;
    public int CurrentTier { get; set; } = -1;
    public bool IsPurchased { get; private set; } = false;

    protected LevelManager levelManager;
    protected Guildhall guildhall;
    protected PopulationManager populationManager;
    protected QuestManager questManager;
    protected MenuManager menuManager;
    protected Menu_UpgradeGuildhall upgradeGuildhall;

    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
        questManager = FindObjectOfType<QuestManager>();
        CheckIsPurchased();
    }

    protected void FixedUpdate()
    {
        if (menuManager == null) menuManager = FindObjectOfType<MenuManager>();
        if (levelManager.CurrentLevel() != "Main") return;
        if (upgradeGuildhall == null) upgradeGuildhall = FindObjectOfType<Menu_UpgradeGuildhall>();
    }

    public void PayForUpgrade()
    {
        Guildhall guildhall = FindObjectOfType<Guildhall>();
        guildhall.AdjustGold(-UpgradeTiers[NextTier()].GoldCost);
        guildhall.AdjustWood(-UpgradeTiers[NextTier()].WoodCost);
        guildhall.AdjustIron(-UpgradeTiers[NextTier()].IronCost);
    }

    public virtual void Apply()
    {
        if (CurrentTier != UpgradeTiers.Length - 1)
        {
            CurrentTier++;
        }
        CheckIsPurchased();
    }

    public bool CanAfford()
    {
        if (guildhall.Gold < UpgradeTiers[NextTier()].GoldCost
            || guildhall.Wood < UpgradeTiers[NextTier()].WoodCost
            || guildhall.Iron < UpgradeTiers[NextTier()].IronCost
            || guildhall.ArtisanProficiency < UpgradeTiers[NextTier()].ArtisanCost)
            return false;
        return true;
    }

    public int NextTier()
    {
        CheckIsPurchased();
        if (IsPurchased)
        {
            return CurrentTier;
        }
        return CurrentTier + 1;
    }

    private void CheckIsPurchased()
    {
        IsPurchased = (CurrentTier == UpgradeTiers.Length - 1);
    }


    /// <summary>
    /// Set the TierUpgrade's CurrentTier. This is only used for loading the game.
    /// </summary>
    /// <param name="tier"></param>
    public void SetCurrentTier(int tier)
    {
        CurrentTier = tier;
    }
}
