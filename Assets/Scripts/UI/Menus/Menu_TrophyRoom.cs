using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_TrophyRoom : Menu
{
    [SerializeField]
    private GameObject trophyPrefab;

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private Image filterIncompleteImage;

    [SerializeField]
    private Image filterCompleteImage;

    [SerializeField]
    private TextMeshProUGUI trophyCountTxt;

    private TrophyManager trophyManager;
    private List<GameObject> trophyObjs = new List<GameObject>();
    private List<TrophyItemRow> trophyItems = new List<TrophyItemRow>();
    private Color selectedColor = new Color(0.72f, 0.72f, 0.72f);
    private int completed = 0;

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
            CountCompleted();
            trophyCountTxt.text = $"Completed {completed} / {trophyItems.Count}";
        }
    }

    public override void Open()
    {
        base.Open();
        scrollbar.value = 1;
    }

    public void FilterIncomplete()
    {
        foreach(GameObject trophyObj in trophyObjs)
        {
            if (trophyObj.GetComponent<TrophyItemRow>().Trophy.IsUnlocked)
                trophyObj.SetActive(false);
            else
                trophyObj.SetActive(true);
        }
        filterIncompleteImage.color = selectedColor;
        filterCompleteImage.color = Color.white;
        scrollbar.value = 1;
    }

    public void FilterComplete()
    {
        foreach (GameObject trophyObj in trophyObjs)
        {
            if (!trophyObj.GetComponent<TrophyItemRow>().Trophy.IsUnlocked)
                trophyObj.SetActive(false);
            else
                trophyObj.SetActive(true);
        }
        filterIncompleteImage.color = Color.white;
        filterCompleteImage.color = selectedColor;
        scrollbar.value = 1;
    }

    public void FilterAll()
    {
        foreach (GameObject trophyObj in trophyObjs)
        {
            trophyObj.SetActive(true);
        }
        filterIncompleteImage.color = Color.white;
        filterCompleteImage.color = Color.white;
        scrollbar.value = 1;
    }

    private void PopulateTrophies()
    {
        trophyObjs.Clear();
        foreach (var trophy in trophyManager.Trophies)
        {
            GameObject trophyObj = Instantiate(trophyPrefab, contentPanel.transform);
            trophyObjs.Add(trophyObj);
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

    private void CountCompleted()
    {
        completed = 0;
        foreach(var trophy in trophyManager.Trophies)
        {
            if (trophy.IsUnlocked)
                completed++;
        }
    }
}
