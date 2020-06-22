using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold, iron, wood, weapons, population, adventurers, artisans, peasants;

    private Guildhall guildhall;
    private PopulationManager populationManager;

    void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void FixedUpdate()
    {
        gold.text = guildhall.GetGold().ToString();
        iron.text = guildhall.GetIron().ToString();
        wood.text = guildhall.GetWood().ToString();
        weapons.text = guildhall.GetWeapons().ToString();

        population.text = populationManager.GuildMembers.Count.ToString();
        adventurers.text = populationManager.Adventurers().Count.ToString();
        artisans.text = populationManager.Artisans().Count.ToString();
        peasants.text = populationManager.Peasants().Count.ToString();
    }
}
