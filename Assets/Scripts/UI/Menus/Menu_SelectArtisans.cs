using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SelectArtisans : Menu
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI artisanProficiencyText;

    [SerializeField]
    private Button beginConstruction;

    [SerializeField]
    PersonUIScrollView scrollView;

    private ConstructionManager constructionManager;
    private Upgrade upgrade;

    #endregion

    protected override void Start()
    {
        base.Start();
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    public override void Open()
    {
        base.Open();
        scrollView.GetAvailableArtisansUI();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            artisanProficiencyText.text = $"{constructionManager.SelectedArtisansProficiency()} / {upgrade.ArtisanCost}";
            SetTextColor();

            if (constructionManager.UnderConstruction || !upgrade.CanAfford())
            {
                beginConstruction.interactable = false;
                beginConstruction.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            }
            else
            {
                beginConstruction.interactable = true;
                beginConstruction.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.196f, 0.196f, 0.196f, 1);
            }
        }    
    }

    public void SetUpgrade(Upgrade _upgrade)
    {
        upgrade = _upgrade;
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(upgrade);
        constructionManager.BeginConstruction();
    }

    private void SetTextColor()
    {
        if (constructionManager.SelectedArtisansProficiency() < upgrade.ArtisanCost)
        {
            artisanProficiencyText.color = Color.red;
        }
        else
        {
            artisanProficiencyText.color = Color.green;
        }
    }
}
