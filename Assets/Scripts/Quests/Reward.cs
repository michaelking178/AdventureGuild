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

    public int BoostGold, BoostWood, BoostIron, BoostExp;

    public Reward(int questLevel, Quest.Skill skill)
    {
        Gold = Random.Range(150 * (questLevel + 1), 150 * (questLevel + 2 + Mathf.FloorToInt(questLevel / 5)));
        Iron = Random.Range(30 * (questLevel + 1), 30 * (questLevel + 2 + Mathf.FloorToInt(questLevel / 5)));
        Wood = Random.Range(50 * (questLevel + 1), 50 * (questLevel + 2 + Mathf.FloorToInt(questLevel / 5)));
        Exp = Random.Range(100 * (questLevel + 1), 100 * (questLevel + 2 + Mathf.FloorToInt(questLevel / 5)));
        Renown = Random.Range(2 * (questLevel + 1), 2 * (questLevel + 2 + Mathf.FloorToInt(questLevel / 5)));
        if (skill != Quest.Skill.None)
        {
            SkillExp = Random.Range(25 * (questLevel + 1), 200 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5)));
        }
        SetBoost();
    }

    public Reward(int _gold, int _iron, int _wood, int _exp, int _renown)
    {
        Gold = _gold;
        Iron = _iron;
        Wood = _wood;
        Exp = _exp;
        Renown = _renown;
    }

    public void SetBoost()
    {
        BoostGold = Mathf.CeilToInt(Gold * GameObject.FindObjectOfType<QuestRewardBoost>().BoostValue);
        BoostWood = Mathf.CeilToInt(Wood * GameObject.FindObjectOfType<QuestRewardBoost>().BoostValue);
        BoostIron = Mathf.CeilToInt(Iron * GameObject.FindObjectOfType<QuestRewardBoost>().BoostValue);
        BoostExp = Mathf.CeilToInt(Exp * GameObject.FindObjectOfType<QuestRewardBoost>().BoostValue);
    }
}
