using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Marketplace : MonoBehaviour
{
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
    private MenuManager menuManager;

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            UpdateMarketplace();
            CalculateTotal();
            SetInteractables();
        }
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

    private void CalculateTotal()
    {
        int sellTotal = (int)(((sellWoodSlider.value * WoodValue) + (sellIronSlider.value * IronValue)) * SellDepreciation);
        int buyTotal = (int)((buyWoodSlider.value * WoodValue) + (buyIronSlider.value * IronValue));
        Total = sellTotal - buyTotal;

        playerGoldText.text = guildhall.Gold.ToString();
        totalText.text = Total.ToString();
        if (Total == 0) totalText.color = Color.white;
        else if (Total > 0) totalText.color = Color.green;
        else totalText.color = Color.red;
    }

    public void Confirm()
    {
        guildhall.AdjustGold(Total);
        guildhall.AdjustWood(woodChange);
        guildhall.AdjustIron(ironChange);

        buyWoodSlider.value = 0;
        buyIronSlider.value = 0;
        sellWoodSlider.value = 0;
        sellIronSlider.value = 0;
    }

    private void DisableSlider(Slider slider)
    {
        slider.value = 0;
        slider.interactable = false;
    }
}
