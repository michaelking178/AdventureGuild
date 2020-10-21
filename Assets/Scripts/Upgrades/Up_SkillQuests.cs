using System.Collections;
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
        StartCoroutine(DelayedCheckForUpgrade());
    }

    public override void Apply()
    {
        base.Apply();
        questManager.UnlockSkill(skill);
    }

    public void CheckForUpgrade()
    {
        if (questManager.IsSkillUnlocked(skill))
        {
            IsPurchased = true;
        }
    }

    private IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade();
        yield return null;
    }
}
