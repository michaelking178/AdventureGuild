using UnityEngine;

public class Challenge : MonoBehaviour
{
    public string Objective { get; set; }
    public int ObjectiveQuantity { get; set; }
    public int Progress { get; set; }
    public Reward Reward { get; protected set; }
    public bool IsCompleted { get; set; }

    private Guildhall guildhall;

    protected void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
    }

    public virtual void EndChallenge()
    {
        Debug.LogWarning($"Challenge {name} has not implemented EndChallenge()!");
    }

    protected void AddProgress(int val)
    {
        Progress += val;
        CheckProgress();
    }

    public void CheckProgress()
    {
        if (Progress >= ObjectiveQuantity)
            Complete();
    }


    protected virtual void Complete()
    {
        IsCompleted = true;
        EndChallenge();
        guildhall.AdjustGold(Reward.Gold);
        guildhall.AdjustIron(Reward.Iron);
        guildhall.AdjustWood(Reward.Wood);
        guildhall.AdjustRenown(Reward.Renown);
        Debug.LogWarning($"Challenge Completed: {name}");
    }
}