using System;

[Serializable]
public class Adventurer : Vocation
{
    public int QuestsCompleted { get; set; } = 0;

    public Adventurer()
    {
        title = "Adventurer";
        MaxLevel = 20;
    }
}
