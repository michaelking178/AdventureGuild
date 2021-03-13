﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Construction : Menu
{
    #region Data

    [SerializeField]
    private Button buildButton;

    [SerializeField]
    private Button selectArtisansButton;

    [SerializeField]
    private TextMeshProUGUI projectText;

    [SerializeField]
    private TextMeshProUGUI goldCostText;

    [SerializeField]
    private TextMeshProUGUI woodCostText;

    [SerializeField]
    private TextMeshProUGUI ironCostText;

    [SerializeField]
    private TextMeshProUGUI artisanCostText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI expText;

    [SerializeField]
    private TextMeshProUGUI upgradeText;

    private Guildhall guildhall;
    private ConstructionManager constructionManager;

    public Upgrade ConstructionJob;

    #endregion

    protected override void Start()
    {
        base.Start();
        guildhall = FindObjectOfType<Guildhall>();
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            Populate();
        }
    }

    public void Populate()
    {
        if (ConstructionJob != null)
        {
            SetButtons();

            if (ConstructionJob.ArtisanCost == 0) artisanCostText.gameObject.SetActive(false);
            else artisanCostText.gameObject.SetActive(true);

            if (ConstructionJob.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (ConstructionJob.WoodCost == 0) woodCostText.gameObject.SetActive(false);
            else woodCostText.gameObject.SetActive(true);

            if (ConstructionJob.GoldCost == 0) goldCostText.gameObject.SetActive(false);
            else goldCostText.gameObject.SetActive(true);

            if (ConstructionJob.IronCost == 0) ironCostText.gameObject.SetActive(false);
            else ironCostText.gameObject.SetActive(true);

            projectText.text = ConstructionJob.Name;

            artisanCostText.text = $"Artisan Proficiency: {ConstructionJob.ArtisanCost}";
            SetColor(artisanCostText, guildhall.ArtisanProficiency, ConstructionJob.ArtisanCost);

            goldCostText.text = $"Gold: {guildhall.Gold} / {ConstructionJob.GoldCost}";
            SetColor(goldCostText, guildhall.Gold, ConstructionJob.GoldCost);

            woodCostText.text = $"Wood: {guildhall.Wood} / {ConstructionJob.WoodCost}";
            SetColor(woodCostText, guildhall.Wood, ConstructionJob.WoodCost);

            ironCostText.text = $"Iron: {guildhall.Iron} / {ConstructionJob.IronCost}";
            SetColor(ironCostText, guildhall.Iron, ConstructionJob.IronCost);

            timeText.text = Helpers.FormatTimer((int)ConstructionJob.constructionTime);
            expText.text = $"{ConstructionJob.Experience} Artisan Experience";
            upgradeText.text = ConstructionJob.Description;
        }
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(ConstructionJob);
        constructionManager.BeginConstruction();
    }

    public void PassUpgradeToSelectArtisans()
    {
        FindObjectOfType<Menu_SelectArtisans>().SetUpgrade(ConstructionJob);
    }

    private void SetColor(TextMeshProUGUI text, int have, int need)
    {
        if (have < need) text.color = Color.red;
        else text.color = Color.green;
    }

    private void SetButtons()
    {
        if (constructionManager.UnderConstruction || !ConstructionJob.CanAfford())
        {
            selectArtisansButton.interactable = false;
            buildButton.interactable = false;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
        }
        else
        {
            selectArtisansButton.interactable = true;
            buildButton.interactable = true;
            selectArtisansButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
            buildButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
        }

        if (ConstructionJob.ArtisanCost == 0)
        {
            buildButton.gameObject.SetActive(true);
            selectArtisansButton.gameObject.SetActive(false);
        }
        else
        {
            buildButton.gameObject.SetActive(false);
            selectArtisansButton.gameObject.SetActive(true);
        }
    }
}