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
        StartCoroutine(DelayedQuestSetBoosts());
    }

    public void SetQuestBoosts()
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

    private IEnumerator DelayedQuestSetBoosts()
    {
        yield return new WaitForSeconds(1);
        SetQuestBoosts();
    }
}
