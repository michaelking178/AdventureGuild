[System.Serializable]
public class Reward
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Exp { get; set; }
    public int Renown { get; set; }

    public Reward(int difficulty)
    {
        Gold = 100 * (difficulty + 1);
        Iron = 10 * (difficulty + 1);
        Wood = 25 * (difficulty + 1);
        Exp = 100 * (difficulty + 1);
        Renown = 15 * (difficulty + 1);
    }
}
