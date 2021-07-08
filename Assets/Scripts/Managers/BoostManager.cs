using System.Collections;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public bool IsQuestExpBoosted = false;
    public bool IsQuestGoldBoosted = false;
    public bool IsQuestWoodBoosted = false;
    public bool IsQuestIronBoosted = false;
    public bool IsTrainingExpBoosted = false;

    private QuestManager questManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        StartCoroutine(DelayedQuestSetBoosts());
    }

    public Boost GetBoost(string _name)
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            if (child.GetComponent<Boost>().Name == _name)
            {
                return child.GetComponent<Boost>();
            }
        }
        Debug.LogWarning("Boost Manager could not find Boost: " + _name);
        return null;
    }

    public void SetQuestBoosts()
    {
        foreach(Quest quest in questManager.GetQuestPool())
        {
            if (quest.State == Quest.Status.New)
            {
                quest.ExpBoosted = IsQuestExpBoosted;
                quest.GoldBoosted = IsQuestGoldBoosted;
                quest.IronBoosted = IsQuestIronBoosted;
                quest.WoodBoosted = IsQuestWoodBoosted;
                quest.Reward.SetBoosts();
            }
        }
    }

    private IEnumerator DelayedQuestSetBoosts()
    {
        yield return new WaitForSeconds(1);
        SetQuestBoosts();
    }
}
