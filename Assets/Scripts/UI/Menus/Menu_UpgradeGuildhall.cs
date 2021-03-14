using UnityEngine;
using UnityEngine.UI;

public class Menu_UpgradeGuildhall : Menu
{
    #region Data

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private GameObject upgradePrefab;

    [SerializeField]
    private Scrollbar scrollbar;

    #endregion

    protected override void Start()
    {
        base.Start();
        PopulateUpgrades();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            CheckForPurchases();
        }
    }

    public override void Open()
    {
        base.Open();
        scrollbar.value = 1;
    }

    private void CheckForPurchases()
    {
        foreach (UpgradeItemFrame upgrade in GetComponentsInChildren<UpgradeItemFrame>())
        {
            upgrade.CheckForPurchase();
        }
    }

    private void PopulateUpgrades()
    {
        foreach (GameObject child in FindObjectOfType<ConstructionManager>().gameObject.GetChildren())
        {
            GameObject upgrade = Instantiate(upgradePrefab, contentPanel.transform);
            upgrade.GetComponent<UpgradeItemFrame>().UpgradeName = child.name;
            upgrade.name = "UpgradeItemFrame_" + child.name;
        }
    }
}
