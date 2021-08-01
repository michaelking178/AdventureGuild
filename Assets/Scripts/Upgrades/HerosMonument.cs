﻿public class HerosMonument : Upgrade
{
    private new void FixedUpdate()
    {
        if (levelManager.CurrentLevel() != "Main") return;

        base.FixedUpdate();
        if (menuManager.CurrentMenu == upgradeGuildhall)
        {
            CheckForUpgrade();
        }
    }

    public override void Apply()
    {
        base.Apply();
        // Do something upgradey. Enable Legend Levelling?
    }

    protected override void CheckForUpgrade()
    {
        // Check if the upgrade is applied ===> IsPurchased = ???;
    }
}
