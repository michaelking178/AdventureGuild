using System;
using TMPro;
using UnityEngine;

public class BoostCountdown : MonoBehaviour
{
    [SerializeField]
    private Menu menu;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private Boost boost;
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        boost = GetComponent<BoostObject>().Boost;
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == menu)
        {
            if (boost != null && boost.BoostRemaining > 0)
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
        timerText.text = Helpers.FormatTimer(Convert.ToInt32(boost.BoostRemaining));
    }

    private void HideTimer()
    {
        timerText.text = "NOT ACTIVE";
    }
}
