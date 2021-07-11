using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Boost))]

public class BoostItemFrame : MonoBehaviour
{
    public Boost Boost;

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

    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        if (Boost != null)
        {
            //boostNameText.text = Boost.Name;
            //descriptionText.text = Boost.Description;
        }
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_WatchAds")
        {
            if (Boost != null && Boost.BoostRemaining > 0)
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
        timerText.text = Helpers.FormatTimer(Convert.ToInt32(Boost.BoostRemaining));
    }

    private void HideTimer()
    {
        timerPanel.SetActive(false);
        timerText.text = "";
    }
}
