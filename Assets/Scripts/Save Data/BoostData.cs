using System;

[Serializable]
public class BoostData
{
    public float BoostRemaining;

    public BoostData()
    {
        BoostRemaining = UnityEngine.GameObject.FindObjectOfType<QuestRewardBoost>().BoostRemaining;
    }
}
