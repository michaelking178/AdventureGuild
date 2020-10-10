using UnityEngine;

public class Up_SkillQuests : Upgrade
{
    [SerializeField]
    private string skill = "";

    private QuestManager questManager;

    private new void Start()
    {
        base.Start();
        questManager = FindObjectOfType<QuestManager>();
        if (questManager.IsSkillUnlocked(skill))
        {
            IsPurchased = true;
        }
    }

    public override void Apply()
    {
        base.Apply();
        questManager.UnlockSkill(skill);
    }
}
