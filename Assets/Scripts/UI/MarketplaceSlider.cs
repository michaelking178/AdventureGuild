using UnityEngine;
using UnityEngine.UI;

public class MarketplaceSlider : MonoBehaviour
{
    public enum SliderType { BuyWood, SellWood, BuyIron, SellIron }
    public SliderType sliderType;
    
    private int resourceCost = 0;
    private float previousValue = 0;
    private Menu_Marketplace marketplace;
    private Guildhall guildhall;
    private Slider slider;

    void Start()
    {
        marketplace = FindObjectOfType<Menu_Marketplace>();
        guildhall = FindObjectOfType<Guildhall>();
        slider = GetComponent<Slider>();

        switch (sliderType)
        {
            case SliderType.BuyWood:
                resourceCost = marketplace.WoodValue;
                break;
            case SliderType.BuyIron:
                resourceCost = marketplace.IronValue;
                break;
            case SliderType.SellWood:
                resourceCost = -(int)(marketplace.WoodValue * marketplace.SellDepreciation);
                break;
            case SliderType.SellIron:
                resourceCost = -(int)(marketplace.IronValue * marketplace.SellDepreciation);
                break;
            default:
                break;
        }
    }

    public void CheckValueLimit(float quantity)
    {
        if (resourceCost * (quantity - previousValue) > (guildhall.Gold + marketplace.Total))
        {
            slider.value = previousValue;
        }
        else
        {
            previousValue = quantity;
        }
    }
}
