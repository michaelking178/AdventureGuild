using System.Collections;
using UnityEngine;

public class SkillQuestUpgrade : Upgrade
{
    [SerializeField]
    private string skill = "";

    private QuestManager questManager;

    private new void Start()
    {
        base.Start();
        questManager = FindObjectOfType<QuestManager>();
    }

    public override void Apply()
    {
        base.Apply();
        questManager.UnlockSkill(skill);
    }

    protected void CheckForUpgrade()
    {
        IsPurchased = questManager.IsSkillUnlocked(skill);
    }
}
