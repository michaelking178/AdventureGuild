using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessHallUpgrade : TierUpgrade
{
    public override void Apply()
    {
        populationManager.ApplyAdventurerHPUpgrade(UpgradeTiers[NextTier()].EffectIncrement);
        base.Apply();
    }
}
