using System.Collections;
using UnityEngine;

public class TreasuryUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
    }

    private new void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        base.FixedUpdate();
        if (menuManager.CurrentMenu == upgradeGuildhall)
        {
            CheckForUpgrade(guildhall.MaxGold);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxGold);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxGold = maxUpgrade;
        if (guildhall.MaxGold < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
