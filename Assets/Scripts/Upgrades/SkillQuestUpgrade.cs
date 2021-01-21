using UnityEngine;

public class SkillQuestUpgrade : Upgrade
{
    [SerializeField]
    private string skill = "";

    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (levelManager.CurrentLevel() == "Title") return;

        if (FindObjectOfType<MenuManager>().CurrentMenu.name == "Menu_UpgradeGuildhall")
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
