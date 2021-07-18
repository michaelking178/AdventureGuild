using System.Collections;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public bool IsQuestRewardBoosted = false;
    public bool IsTrainingExpBoosted = false;

    private QuestManager questManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        Invoke("SetQuestBoost", 1.0f);
    }

    public void SetQuestBoost()
    {
        foreach(Quest quest in questManager.GetQuestPool())
        {
            if (quest.State == Quest.Status.New)
            {
                quest.RewardBoosted = IsQuestRewardBoosted;
                quest.Reward.SetBoost();
            }
        }
    }
}
