using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallOfTrials : Upgrade
{
    private ChallengeManager challengeManager;

    protected new void Start()
    {
        base.Start();
        challengeManager = FindObjectOfType<ChallengeManager>();
    }

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
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = challengeManager.ChallengesUnlocked;
    }
}
