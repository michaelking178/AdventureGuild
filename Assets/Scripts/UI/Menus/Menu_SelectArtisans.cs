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
    private Upgrade upgrade;

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
            artisanProficiencyText.text = $"Artisan Proficiency: {constructionManager.SelectedArtisansProficiency()} / {upgrade.ArtisanCost}";
            SetTextColor();

            if (constructionManager.UnderConstruction || !upgrade.CanAfford() || constructionManager.Artisans.Count < upgrade.ArtisanCost)
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

    public void SetUpgrade(Upgrade _upgrade)
    {
        upgrade = _upgrade;
    }

    public void BeginConstruction()
    {
        constructionManager.SetConstructionJob(upgrade);
        constructionManager.BeginConstruction();
        FindObjectOfType<Menu_UpgradeGuildhall>().Open();
    }

    private void SetTextColor()
    {
        if (constructionManager.SelectedArtisansProficiency() < upgrade.ArtisanCost)
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
