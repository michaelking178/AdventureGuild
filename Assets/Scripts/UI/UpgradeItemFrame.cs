﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFrame : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image checkmarkImage;

    [SerializeField]
    private GameObject timerPanel;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color unavailableColor = new Color(1, 1, 1, 0.25f);
    private Color fontAvailableColor = new Color(0.196f, 0.196f, 0.196f, 1);
    private Color fontUnavailableColor = new Color(0.196f, 0.196f, 0.196f, 0.5f);

    private Sprite itemSprite;
    private Upgrade upgrade;
    private PopupManager popupManager;
    private MenuManager menuManager;
    private ConstructionManager constructionManager;
    private bool isAvailable = true;

    private void Start()
    {
        popupManager = FindObjectOfType<PopupManager>();
        menuManager = FindObjectOfType<MenuManager>();
        constructionManager = FindObjectOfType<ConstructionManager>();
        upgrade = GetComponent<Upgrade>();
        itemSprite = itemImage.sprite;
        itemNameText.text = upgrade.Name;
        StartCoroutine(DelayedCheckForPurchase());
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu.name == "Menu_UpgradeGuildhall")
        {
            CheckForPurchase();
            itemNameText.text = upgrade.Name;

            if (constructionManager.ConstructionJob != null && constructionManager.ConstructionJob.name == upgrade.name && constructionManager.UnderConstruction)
            {
                DisplayTimer();
            }
        }
    }

    public void CheckForPurchase()
    {
        if (upgrade != null && !upgrade.IsPurchased && upgrade.CanAfford())
        {
            SetAvailable();
        }
        else if (upgrade != null && upgrade.IsPurchased)
        {
            SetPurchased();
        }
        else
        {
            SetUnavailable();
        }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetAvailable()
    {
        isAvailable = true;
        itemImage.color = availableColor;
        itemNameText.color = fontAvailableColor;
    }

    public void SetUnavailable()
    {
        isAvailable = false;
        itemImage.color = unavailableColor;
        itemNameText.color = fontUnavailableColor;
    }

    public void SetPurchased()
    {
        SetUnavailable();
        checkmarkImage.gameObject.SetActive(true);
    }

    public void CallPopup()
    {
        if (upgrade.IsPurchased)
        {
            upgrade.Description += "\n\n Purchased!";
            popupManager.CreateDefaultContent(upgrade.Description);
        }
        else
        {
            popupManager.CreateUpgradeContent(upgrade.Description, upgrade.GoldCost, upgrade.WoodCost, upgrade.IronCost, upgrade.ArtisanCost);
        }

        if (isAvailable)
        {
            popupManager.SetDoubleButton("Purchase", "Cancel");
            popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(Confirm);
        }
        else
        {
            popupManager.SetSingleButton("Cancel");
        }
        popupManager.Populate(upgrade.Name, itemSprite);
        popupManager.CallPopup();
    }

    public void SelectUpgrade()
    {
        Menu_Construction construction = FindObjectOfType<Menu_Construction>();
        construction.SetUpgrade(upgrade);
        construction.Populate();
        menuManager.OpenMenu(construction.gameObject.name);
    }

    public void DisplayTimer()
    {
        timerPanel.SetActive(true);
        timerText.text = Helpers.FormatTimer((int)(upgrade.constructionTime - constructionManager.TimeElapsed));
    }

    private IEnumerator DelayedCheckForPurchase()
    {
        yield return new WaitForSeconds(1.5f);
        CheckForPurchase();
        yield return null;
    }

    private void Confirm()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(Confirm);
        GetComponent<Upgrade>().Apply();
    }
}
