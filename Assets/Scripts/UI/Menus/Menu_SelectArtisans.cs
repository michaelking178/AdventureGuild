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

    protected override void Start()
    {
        base.Start();
        constructionManager = FindObjectOfType<ConstructionManager>();
        scrollView = GetComponentInChildren<PersonUIScrollView>();
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

    public override void Open()
    {
        base.Open();
        scrollView.LoadAvailablePersonUIs();
        scrollView.SetPersonUIButtons(true, false, false);
        scrollbar.value = 1;
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Expand();
        }
    }

    public override void Close()
    {
        base.Close();
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
