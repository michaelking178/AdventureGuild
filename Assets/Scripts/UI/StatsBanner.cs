using TMPro;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold, iron, wood, population, adventurers, artisans, peasants, renown, artisanProficiency;

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
        artisanProficiency.text = $"Artisan Proficiency: {guildhall.ArtisanProficiency}";
    }

    public void Extend()
    {
        if (anim.GetBool("IsExtended")) anim.SetBool("IsExtended", false);
        else anim.SetBool("IsExtended", true);
    }
}
