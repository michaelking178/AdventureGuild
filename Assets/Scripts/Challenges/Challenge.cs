using UnityEngine;

public class Challenge : MonoBehaviour
{
    public string Objective { get; protected set; }
    public int ObjectiveQuantity { get; protected set; }
    public int Progress { get; protected set; }
    public Reward Reward { get; protected set; }
    public bool IsCompleted { get; protected set; }

    private Guildhall guildhall;

    protected void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
    }

    public virtual void EndChallenge()
    {
        Debug.LogWarning($"Challenge {name} has not implemented EndChallenge()!");
    }

    protected void AddProgress(int value)
    {
        Progress += value;
        if (Progress >= ObjectiveQuantity)
            Complete();
    }

    protected virtual void Complete()
    {
        IsCompleted = true;
        guildhall.AdjustGold(Reward.Gold);
        guildhall.AdjustIron(Reward.Iron);
        guildhall.AdjustWood(Reward.Wood);
        guildhall.AdjustRenown(Reward.Renown);
        EndChallenge();
        Debug.LogWarning($"Challenge Completed: {name}");
    }
}