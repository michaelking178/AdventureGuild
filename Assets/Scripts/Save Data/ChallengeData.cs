[System.Serializable]
public class ChallengeData
{
    public string Objective;
    public int ObjectiveQuantity;
    public int Progress;
    public string Name;
    public bool IsCompleted;

    public ChallengeData(Challenge challenge)
    {
        Name = challenge.name;
        Objective = challenge.Objective;
        ObjectiveQuantity = challenge.ObjectiveQuantity;
        Progress = challenge.Progress;
        IsCompleted = challenge.IsCompleted;
    }
}
