using System.Collections;
using UnityEngine;

public class LumberCampUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>().CurrentMenu.name == "Menu_UpgradeGuildhall")
        {
            CheckForUpgrade(guildhall.MaxWoodIncome);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxWoodIncome);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxWoodIncome = maxUpgrade;
        if (guildhall.MaxWoodIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
