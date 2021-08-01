using TMPro;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold, iron, wood, population, adventurers, artisans, peasants, renown,
        artisanProficiency, maxGold, maxIron, maxWood, currentGoldIncome, currentIronIncome, currentWoodIncome,
        maxGoldIncome, maxIronIncome, maxWoodIncome, timeToMaxGold, timeToMaxIron, timeToMaxWood, legendLevel, legendLevelVal;

    [Header("Audio")]
    [SerializeField]
    private AudioClip openSwoosh;

    [SerializeField]
    private AudioClip closeSwoosh;

    private Guildhall guildhall;
    private PopulationManager populationManager;
    private SoundManager soundManager;
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
        soundManager = FindObjectOfType<SoundManager>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = soundManager.GetComponent<AudioSource>().volume;
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
        currentGoldIncome.text = string.Format("{0:n0}/hr", guildhall.GoldIncome);
        currentIronIncome.text = string.Format("{0:n0}/hr", guildhall.IronIncome);
        currentWoodIncome.text = string.Format("{0:n0}/hr", guildhall.WoodIncome);
        maxGoldIncome.text = string.Format("{0:n0}", guildhall.MaxGoldIncome);
        maxIronIncome.text = string.Format("{0:n0}", guildhall.MaxIronIncome);
        maxWoodIncome.text = string.Format("{0:n0}", guildhall.MaxWoodIncome);

        if (guildhall.GoldIncome == 0) timeToMaxGold.text = "Gold: N/A";
        else timeToMaxGold.text = "Gold: " + CalculateTimeToMax(guildhall.GoldIncome, guildhall.MaxGoldIncome).ToString() + " hours";

        if (guildhall.IronIncome == 0) timeToMaxIron.text = "Iron: N/A";
        else timeToMaxIron.text = "Iron: " + CalculateTimeToMax(guildhall.IronIncome, guildhall.MaxIronIncome).ToString() + " hours";

        if (guildhall.WoodIncome == 0) timeToMaxWood.text = "Wood: N/A";
        else timeToMaxWood.text = "Wood: " + CalculateTimeToMax(guildhall.WoodIncome, guildhall.MaxWoodIncome).ToString() + " hours";

        if (guildhall.LegendLevel == 0)
        {
            legendLevel.text = "";
            legendLevelVal.text = "";
        }
        else
        {
            legendLevel.text = "Legend Level:";
            legendLevelVal.text = guildhall.LegendLevel.ToString();
        }
    }

    public void Extend()
    {
        if (anim.GetBool("IsExtended"))
        {
            anim.SetBool("IsExtended", false);
            audioSource.clip = closeSwoosh;
            audioSource.Play();
        }
        else
        {
            anim.SetBool("IsExtended", true);
            audioSource.clip = openSwoosh;
            audioSource.Play();
        }
    }

    private int CalculateTimeToMax(int incomePerHour, int maxResources)
    {
        if (incomePerHour == 0) return 0;
        return Mathf.CeilToInt((float)maxResources / incomePerHour);
    }
}
