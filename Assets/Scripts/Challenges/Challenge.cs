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
        Progress = 0;
        IsCompleted = true;
    }

    public virtual void ResetChallenge()
    {
        EndChallenge();
        IsCompleted = false;
    }

    protected void AddProgress(int val)
    {
        if (!IsCompleted)
        {
            Progress += val;
            CheckProgress();
        }
    }

    public void CheckProgress()
    {
        if (!IsCompleted && Progress >= ObjectiveQuantity)
            Complete();
    }

    protected virtual void Complete()
    {
        EndChallenge();
        guildhall.AdjustGold(Reward.Gold);
        guildhall.AdjustIron(Reward.Iron);
        guildhall.AdjustWood(Reward.Wood);
        guildhall.AdjustRenown(Reward.Renown);
    }
}