using System.Collections;
using UnityEngine;

public class StorageYardUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>() != null && FindObjectOfType<MenuManager>().CurrentMenu == FindObjectOfType<Menu_UpgradeGuildhall>())
        {
            CheckForUpgrade(guildhall.MaxWood);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxWood);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxWood = maxUpgrade;
        if (guildhall.MaxWood < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
