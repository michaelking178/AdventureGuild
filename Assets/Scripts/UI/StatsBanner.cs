using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold, iron, wood, weapons, population, adventurers, artisans, peasants;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGold(1500);
        UpdateIron(245);
        UpdateWood(750);
        UpdateWeapons(45);
        UpdatePopulation(22);
        UpdateAdventurers(3);
        UpdateArtisans(7);
        UpdatePeasants(12);
    }

    public void UpdateGold(int num)
    {
        gold.text = num.ToString();
    }

    public void UpdateIron(int num)
    {
        iron.text = num.ToString();
    }

    public void UpdateWood(int num)
    {
        wood.text = num.ToString();
    }

    public void UpdateWeapons(int num)
    {
        weapons.text = num.ToString();
    }

    public void UpdatePopulation(int num)
    {
        population.text = num.ToString();
    }

    public void UpdateAdventurers(int num)
    {
        adventurers.text = num.ToString();
    }

    public void UpdateArtisans(int num)
    {
        artisans.text = num.ToString();
    }

    public void UpdatePeasants(int num)
    {
        peasants.text = num.ToString();
    }
}
