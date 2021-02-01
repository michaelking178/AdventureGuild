using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostItemFrame : MonoBehaviour
{
    public Boost boost;

    [SerializeField]
    private string BoostName;

    [SerializeField]
    private Image boostImage;

    [SerializeField]
    private GameObject timerPanel;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI boostNameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private BoostManager boostManager;
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        boostManager = FindObjectOfType<BoostManager>();
        boostNameText.text = boost.Name;
        descriptionText.text = boost.Description;
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_WatchAds")
        {
            if (/* boost is active */ true)
            {
                DisplayTimer();
            }
            else
            {
                HideTimer();
            }
        }
    }

    private void DisplayTimer()
    {
        timerPanel.SetActive(true);
        timerText.text = Helpers.FormatTimer(Convert.ToInt32(boost.BoostRemaining));
    }

    private void HideTimer()
    {
        timerPanel.SetActive(false);
        timerText.text = "";
    }
}
