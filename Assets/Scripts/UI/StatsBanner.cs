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
        renown.text = guildhall.Renown.ToString();

        artisanProficiency.text = guildhall.ArtisanProficiency.ToString();
        maxGold.text = string.Format("{0:n0}", guildhall.MaxGold);
        maxIron.text = string.Format("{0:n0}", guildhall.MaxIron);
        maxWood.text = string.Format("{0:n0}", guildhall.MaxWood);
        currentGoldIncome.text = string.Format("{0:n0}", guildhall.GoldIncome) + "/hr";
        currentIronIncome.text = string.Format("{0:n0}", guildhall.IronIncome) + "/hr";
        currentWoodIncome.text = string.Format("{0:n0}", guildhall.WoodIncome) + "/hr";
        maxGoldIncome.text = string.Format("{0:n0}", guildhall.MaxGoldIncome);
        maxIronIncome.text = string.Format("{0:n0}", guildhall.MaxIronIncome);
        maxWoodIncome.text = string.Format("{0:n0}", guildhall.MaxWoodIncome);

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
