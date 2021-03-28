using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Marketplace : Menu
{
    #region Data

    [Header("Economy Values")]
    public int WoodValue = 100;
    public int IronValue = 250;
    [Space]
    public int MaxWoodInventory = 100;
    public int MaxIronInventory = 50;
    [Space]
    public float SellDepreciation = 0.75f;

    [Header("GameObjects")]
    [SerializeField]
    private Slider buyWoodSlider;

    [SerializeField]
    private Slider buyIronSlider;
    
    [SerializeField]
    private Slider sellWoodSlider;

    [SerializeField]
    private Slider sellIronSlider;

    [SerializeField]
    private TextMeshProUGUI buyWoodText, buyIronText, sellWoodText, sellIronText, playerGoldText, totalText;

    private Guildhall guildhall;

    [HideInInspector]
    public int Total = 0;

    private int woodChange = 0, ironChange = 0;
    private Color green = new Color(0, 0.4f, 0, 1);

    #endregion

    protected override void Start()
    {
        base.Start();
        guildhall = FindObjectOfType<Guildhall>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            UpdateMarketplace();
            CalculateTotal();
            SetInteractables();
        }
    }

    public void UpdateMarketplace()
    {
        buyWoodSlider.maxValue = MaxWoodInventory / 10;
        buyWoodText.text = (buyWoodSlider.value * 10).ToString() + "/" + MaxWoodInventory;

        buyIronSlider.maxValue = MaxIronInventory / 10;
        buyIronText.text = (buyIronSlider.value * 10).ToString() + "/" + MaxIronInventory;

        sellWoodSlider.maxValue = GetWoodMax() / 10;
        sellWoodText.text = (sellWoodSlider.value * 10).ToString() + "/" + guildhall.Wood.ToString();

        sellIronSlider.maxValue = GetIronMax() / 10;
        sellIronText.text = (sellIronSlider.value * 10).ToString() + "/" + guildhall.Iron.ToString();

        woodChange = (int)(buyWoodSlider.value - sellWoodSlider.value) * 10;
        ironChange = (int)(buyIronSlider.value - sellIronSlider.value) * 10;
    }

    public int GetWoodMax()
    {
        return guildhall.Wood - (guildhall.Wood % 10);
    }

    public int GetIronMax()
    {
        return guildhall.Iron - (guildhall.Iron % 10);
    }

    public void Confirm()
    {
        guildhall.AdjustGold(Total);
        guildhall.AdjustWood(woodChange);
        guildhall.AdjustIron(ironChange);
        ResetSliders();
    }

    public void ResetSliders()
    {
        buyWoodSlider.GetComponent<MarketplaceSlider>().ResetSlider();
        buyIronSlider.GetComponent<MarketplaceSlider>().ResetSlider();
        sellWoodSlider.GetComponent<MarketplaceSlider>().ResetSlider();
        sellIronSlider.GetComponent<MarketplaceSlider>().ResetSlider();
    }

    private void SetInteractables()
    {
        if (buyWoodSlider.value > 0 || sellWoodSlider.maxValue == 0)
        {
            DisableSlider(sellWoodSlider);
        }
        else if (sellWoodSlider.value > 0 || buyWoodSlider.maxValue == 0)
        {
            DisableSlider(buyWoodSlider);
        }
        else
        {
            sellWoodSlider.interactable = true;
            buyWoodSlider.interactable = true;
        }

        if (buyIronSlider.value > 0 || sellIronSlider.maxValue == 0)
        {
            DisableSlider(sellIronSlider);
        }
        else if (sellIronSlider.value > 0 || buyIronSlider.maxValue == 0)
        {
            DisableSlider(buyIronSlider);
        }
        else
        {
            sellIronSlider.interactable = true;
            buyIronSlider.interactable = true;
        }
    }

    private void CalculateTotal()
    {
        int sellTotal = (int)(((sellWoodSlider.value * WoodValue) + (sellIronSlider.value * IronValue)) * SellDepreciation);
        int buyTotal = (int)((buyWoodSlider.value * WoodValue) + (buyIronSlider.value * IronValue));
        Total = sellTotal - buyTotal;

        playerGoldText.text = guildhall.Gold.ToString();
        totalText.text = Total.ToString();
        if (Total == 0) totalText.color = Color.black;
        else if (Total > 0) totalText.color = green;
        else totalText.color = Color.red;
    }

    private void DisableSlider(Slider slider)
    {
        slider.value = 0;
        slider.interactable = false;
    }
}
