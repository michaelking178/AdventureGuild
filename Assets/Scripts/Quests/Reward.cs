using UnityEngine;

[System.Serializable]
public class Reward
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Exp { get; set; }
    public int Renown { get; set; }

    public Reward(int questLevel)
    {
        Gold = 100 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Iron = 10 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Wood = 25 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Exp = 100 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
        Renown = 2 * (questLevel + 1 + Mathf.FloorToInt(questLevel / 5));
    }
}
