using UnityEngine;

public class Up_SkillQuests : MonoBehaviour, IUpgrade
{
    [SerializeField]
    private string skill = "";

    public void Apply()
    {
        FindObjectOfType<QuestManager>().UnlockSkill(skill);
    }
}
