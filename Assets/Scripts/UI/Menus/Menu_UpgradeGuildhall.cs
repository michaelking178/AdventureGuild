using UnityEngine;

public class Menu_UpgradeGuildhall : MonoBehaviour
{
    public void CheckForPurchases()
    {
        foreach (UpgradeItemFrame upgrade in GetComponentsInChildren<UpgradeItemFrame>())
        {
            upgrade.CheckForPurchase();
        }
    }
}
