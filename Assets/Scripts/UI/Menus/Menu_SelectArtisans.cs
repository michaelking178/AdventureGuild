using System.Collections;
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
    private Scrollbar scrollbar;

    private PersonUIScrollView scrollView;
    private ConstructionManager constructionManager;
    private TierUpgrade upgrade;
    private TierUpgradeObject upgradeTier;

    #endregion

    private void Start()
    {
        constructionManager = FindObjectOfType<ConstructionManager>();
        scrollView = GetComponentInChildren<PersonUIScrollView>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            artisanProficiencyText.text = $"Artisan Proficiency: {constructionManager.SelectedArtisansProficiency()} / {upgradeTier.ArtisanCost}";
            SetTextColor();

            if (constructionManager.UnderConstruction || !upgrade.CanAfford() || constructionManager.SelectedArtisansProficiency() < upgradeTier.ArtisanCost)
                beginConstruction.interactable = false;
            else
                beginConstruction.interactable = true;
        }    
    }

    public override void Open()
    {
        base.Open();
        scrollView.LoadAvailablePersonUIs();
        scrollView.SetPersonUIButtons(true, false, false, "Select Artisan");
        scrollbar.value = 1;
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Expand();
        }
    }

    public override void Close()
    {
        base.Close();
        if (!constructionManager.UnderConstruction)
            constructionManager.Artisans.Clear();
        StartCoroutine(ClearPersonUIs());
    }

    public void SetUpgrade(TierUpgrade _upgrade)
    {
        upgrade = _upgrade;
        upgradeTier = upgrade.UpgradeTiers[upgrade.CurrentTier + 1];
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(upgrade);
        constructionManager.BeginConstruction();
        FindObjectOfType<Menu_UpgradeGuildhall>().Open();
    }

    private void SetTextColor()
    {
        if (constructionManager.SelectedArtisansProficiency() < upgradeTier.ArtisanCost)
            artisanProficiencyText.color = Color.red;
        else
            artisanProficiencyText.color = Color.green;
    }

    private IEnumerator ClearPersonUIs()
    {
        yield return new WaitForSeconds(1);
        scrollView.ClearPersonUIs();
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Collapse();
        }
    }
}
