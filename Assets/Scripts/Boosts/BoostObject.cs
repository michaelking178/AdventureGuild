using UnityEngine;

public class BoostObject : MonoBehaviour
{
    public Boost Boost { get; private set; }

    [SerializeField]
    private BoostType boostType;
    private enum BoostType { QuestReward, TrainingXP, AdventurerHP };

    private void Awake()
    {
        if (boostType == BoostType.QuestReward)
            Boost = FindObjectOfType<QuestRewardBoost>();
        else if (boostType == BoostType.TrainingXP)
            Boost = FindObjectOfType<TrainingXPBoost>();
        else if (boostType == BoostType.AdventurerHP)
            Boost = FindObjectOfType<AdventurerRecoveryBoost>();
    }
}
