using System.Collections;
using UnityEngine;

public class IronMineUpgrade : MaxResourceUpgrade
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
            CheckForUpgrade(guildhall.MaxIronIncome);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxIronIncome);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxIronIncome = maxUpgrade;
        if (guildhall.MaxIronIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
