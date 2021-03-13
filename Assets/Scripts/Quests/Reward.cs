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
        Gold = Random.Range(200 * (questLevel + 1), 200 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        Iron = Random.Range(30 * (questLevel + 1), 30 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        Wood = Random.Range(50 * (questLevel + 1), 50 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        Exp = Random.Range(100 * (questLevel + 1), 100 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        Renown = Random.Range(2 * (questLevel + 1), 2 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        if (skill != Quest.Skill.None)
        {
            SkillExp = Random.Range(25 * (questLevel + 1), 200 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        }
    }
}
