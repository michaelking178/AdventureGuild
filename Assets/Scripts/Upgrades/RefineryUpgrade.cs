using System.Collections;
using UnityEngine;

public class RefineryUpgrade : MaxResourceUpgrade
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
            CheckForUpgrade(guildhall.MaxIron);
        }
    }

    protected override IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade(guildhall.MaxIron);
        yield return null;
    }

    public override void Apply()
    {
        base.Apply();
        guildhall.MaxIron = maxUpgrade;
        if (guildhall.MaxIron < LevelFiveUpgrade)
        {
            IsPurchased = false;
        }
    }
}
