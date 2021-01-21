using System.Collections;
using UnityEngine;

public class RefineryUpgrade : MaxResourceUpgrade
{
    private new void Start()
    {
        base.Start();
        StartCoroutine(DelayedCheckForUpgrade());
    }

    private void Update()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>().CurrentMenu.name == "Menu_UpgradeGuildhall")
        {
            CheckForUpgrade(guildhall.MaxIron);
        }
    }

    private IEnumerator DelayedCheckForUpgrade()
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
