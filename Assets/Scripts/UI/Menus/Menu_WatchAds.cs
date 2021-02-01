using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_WatchAds : MonoBehaviour
{
    private MenuManager menuManager;

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private GameObject boostFramePrefab;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        PopulateBoosts();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            // CheckForActiveBoosts
        }
    }

    private void PopulateBoosts()
    {
        foreach (GameObject child in FindObjectOfType<BoostManager>().gameObject.GetChildren())
        {
            GameObject boostFrame = Instantiate(boostFramePrefab, contentPanel.transform);
            boostFrame.GetComponent<BoostItemFrame>().boost = child.GetComponent<Boost>();
            boostFrame.name = "BoostFrame_" + child.name;
        }
    }
}
