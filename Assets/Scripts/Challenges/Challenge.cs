using UnityEngine;

public class Challenge : MonoBehaviour
{
    public string Objective { get; protected set; }
    public int ObjectiveQuantity { get; protected set; }
    public int Progress { get; protected set; }
    protected Reward Reward;
}
