using UnityEngine;

public class SkillQuestUpgrade : TierUpgrade
{
    [SerializeField]
    private string skill = "";

    public override void Apply()
    {
        questManager.UnlockSkill(skill);
        base.Apply();
    }
}
