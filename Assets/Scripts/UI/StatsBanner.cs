using TMPro;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold, iron, wood, population, adventurers, artisans, peasants, renown,
        artisanProficiency, maxGold, maxIron, maxWood, currentGoldIncome, currentIronIncome, currentWoodIncome,
        maxGoldIncome, maxIronIncome, maxWoodIncome, timeToMaxGold, timeToMaxIron, timeToMaxWood;

    private Guildhall guildhall;
    private PopulationManager populationManager;
    private Animator anim;

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        gold.text = guildhall.Gold.ToString();
        iron.text = guildhall.Iron.ToString();
        wood.text = guildhall.Wood.ToString();

        population.text = populationManager.GuildMembers.Count.ToString() + "/" + populationManager.PopulationCap.ToString();
        adventurers.text = populationManager.Adventurers().Count.ToString();
        artisans.text = populationManager.Artisans().Count.ToString();
        peasants.text = populationManager.Peasants().Count.ToString();
        renown.text = guildhall.Renown.ToString() + "/" + guildhall.RenownThreshold.ToString();

        artisanProficiency.text = guildhall.ArtisanProficiency.ToString();
        maxGold.text = guildhall.MaxGold.ToString();
        maxIron.text = guildhall.MaxIron.ToString();
        maxWood.text = guildhall.MaxWood.ToString();
        currentGoldIncome.text = guildhall.GoldIncome.ToString() + "/hr";
        currentIronIncome.text = guildhall.IronIncome.ToString() + "/hr";
        currentWoodIncome.text = guildhall.WoodIncome.ToString() + "/hr";
        maxGoldIncome.text = guildhall.MaxGoldIncome.ToString();
        maxIronIncome.text = guildhall.MaxIronIncome.ToString();
        maxWoodIncome.text = guildhall.MaxWoodIncome.ToString();

        if (guildhall.GoldIncome == 0) timeToMaxGold.text = "Gold: N/A";
        else timeToMaxGold.text = "Gold: " + CalculateTimeToMax(guildhall.GoldIncome, guildhall.MaxGoldIncome).ToString() + " hours";

        if (guildhall.IronIncome == 0) timeToMaxIron.text = "Iron: N/A";
        else timeToMaxIron.text = "Iron: " + CalculateTimeToMax(guildhall.IronIncome, guildhall.MaxIronIncome).ToString() + " hours";

        if (guildhall.WoodIncome == 0) timeToMaxWood.text = "Wood: N/A";
        else timeToMaxWood.text = "Wood: " + CalculateTimeToMax(guildhall.WoodIncome, guildhall.MaxWoodIncome).ToString() + " hours";
    }

    public void Extend()
    {
        if (anim.GetBool("IsExtended")) anim.SetBool("IsExtended", false);
        else anim.SetBool("IsExtended", true);
    }

    private int CalculateTimeToMax(int incomePerHour, int maxResources)
    {
        if (incomePerHour == 0) return 0;
        return Mathf.CeilToInt((float)maxResources / incomePerHour);
    }
}
