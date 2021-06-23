using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_TrophyRoom : Menu
{
    [SerializeField]
    private GameObject trophyPrefab;

    [SerializeField]
    private GameObject contentPanel;

    private TrophyManager trophyManager;
    private List<TrophyItemRow> trophyItems = new List<TrophyItemRow>();

    private void Start()
    {
        trophyManager = FindObjectOfType<TrophyManager>();
        PopulateTrophies();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            UpdateTrophies();
        }
    }

    private void PopulateTrophies()
    {
        foreach (var trophy in trophyManager.Trophies)
        {
            GameObject trophyObj = Instantiate(trophyPrefab, contentPanel.transform);
            trophyObj.GetComponent<TrophyItemRow>().SetTrophy(trophy);
            trophyItems.Add(trophyObj.GetComponent<TrophyItemRow>());
        }
    }

    private void UpdateTrophies()
    {
        foreach (var trophyItem in trophyItems)
        {
            trophyItem.UpdateTrophyItem();
        }
    }
}
