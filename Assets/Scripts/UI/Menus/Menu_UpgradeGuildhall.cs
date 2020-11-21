using UnityEngine;

public class Menu_UpgradeGuildhall : MonoBehaviour
{
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
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
}
