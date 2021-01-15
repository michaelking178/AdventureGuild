using UnityEngine;

public class Menu_UpgradeGuildhall : MonoBehaviour
{
    private MenuManager menuManager;

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private GameObject upgradePrefab;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        PopulateUpgrades();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            CheckForPurchases();
        }
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
        foreach (GameObject child in Helpers.GetChildren(FindObjectOfType<ConstructionManager>().gameObject))
        {
            GameObject upgrade = Instantiate(upgradePrefab, contentPanel.transform);
            upgrade.GetComponent<UpgradeItemFrame>().UpgradeName = child.name;
            upgrade.name = "UpgradeItemFrame_" + child.name;
        }
    }
}
