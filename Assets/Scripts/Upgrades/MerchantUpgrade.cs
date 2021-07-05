using System.Collections;
using UnityEngine;

public class MerchantUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    private new void FixedUpdate()
    {
        if (levelManager.CurrentLevel() != "Main") return;

        base.FixedUpdate();
        if (menuManager.CurrentMenu == upgradeGuildhall)
        {
            CheckForUpgrade(guildhall.MaxGoldIncome);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxGoldIncome);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxGoldIncome = maxUpgrade;
        if (guildhall.MaxGoldIncome < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
