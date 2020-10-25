using UnityEngine;

[System.Serializable]
public class Reward
{
    public int Gold { get; set; } = 0;
    public int Iron { get; set; } = 0;
    public int Wood { get; set; } = 0;
    public int Exp { get; set; } = 0;
    public int Renown { get; set; } = 0;
    public int SkillExp { get; set; } = 0;

    public Reward(int questLevel, Quest.Skill skill)
    {
        Gold = 100 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Iron = 10 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Wood = 25 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Exp = 100 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Renown = 2 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        if (skill != Quest.Skill.None)
        {
            SkillExp = 25 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        }
    }
}
