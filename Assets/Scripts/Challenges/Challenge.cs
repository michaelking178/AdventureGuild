using UnityEngine;

public class Challenge : MonoBehaviour
{
    public string Objective { get; protected set; }
    public int ObjectiveQuantity { get; protected set; }
    public int Progress { get; protected set; }
    public Reward Reward { get; protected set; }
    public bool IsCompleted { get; protected set; }

    public virtual void EndChallenge()
    {
        Debug.LogWarning($"Challenge {name} has not implemented EndChallenge()!");
    }

    protected void AddProgress(int value)
    {
        Progress += value;
        if (Progress == ObjectiveQuantity)
            Complete();
        Debug.LogWarning($"Progess triggered: {value}");
    }

    protected virtual void Complete()
    {
        IsCompleted = true;
        Debug.LogWarning($"Challenge Completed: {name}");
    }
}