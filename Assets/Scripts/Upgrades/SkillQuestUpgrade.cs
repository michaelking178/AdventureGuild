using UnityEngine;

public class SkillQuestUpgrade : Upgrade
{
    [SerializeField]
    private string skill = "";

    private new void Start()
    {
        base.Start();
    }

    private new void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        base.FixedUpdate();
        if (menuManager.CurrentMenu == upgradeGuildhall)
        {
            CheckForUpgrade();
        }
    }

    public override void Apply()
    {
        base.Apply();
        questManager.UnlockSkill(skill);
    }

    protected override void CheckForUpgrade()
    {
        IsPurchased = questManager.IsSkillUnlocked(skill);
    }
}
