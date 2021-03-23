using System;

[Serializable]
public class BoostData
{
    public float expBoostRemaining;
    public float goldBoostRemaining;
    public float woodBoostRemaining;
    public float ironBoostRemaining;

    public BoostData()
    {
        expBoostRemaining = UnityEngine.GameObject.FindObjectOfType<QuestExpBoost>().BoostRemaining;
        goldBoostRemaining = UnityEngine.GameObject.FindObjectOfType<QuestGoldBoost>().BoostRemaining;
        woodBoostRemaining = UnityEngine.GameObject.FindObjectOfType<QuestWoodBoost>().BoostRemaining;
        ironBoostRemaining = UnityEngine.GameObject.FindObjectOfType<QuestIronBoost>().BoostRemaining;
    }
}
