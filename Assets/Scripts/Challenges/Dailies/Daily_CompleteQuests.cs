using UnityEngine;

public class Daily_CompleteQuests : DailyChallenge
{
    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = Random.Range(3, 6);
        Objective = $"Complete {ObjectiveQuantity} quests";
        QuestManager.OnQuestCompleted += AddProgress;
    }

    public override void EndChallenge()
    {
        QuestManager.OnQuestCompleted -= AddProgress;
    }
}
