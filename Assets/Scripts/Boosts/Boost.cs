using UnityEngine;

public class Boost : MonoBehaviour
{
    protected BoostManager boostManager;
    public string Name;
    public string Description;
    public float Duration = 7200;
    public float MaxDuration = 21600;
    public float BoostRemaining = 0;
    public float BoostValue = 1.2f;
    
    protected void Start()
    {
        boostManager = FindObjectOfType<BoostManager>();
    }

    protected void FixedUpdate()
    {
        if (BoostRemaining > 0)
        {
            BoostRemaining -= Time.deltaTime;
            SetBoostBool(true);
        }
        else
        {
            BoostRemaining = 0;
            SetBoostBool(false);
        }
    }

    protected virtual void SetBoostBool(bool value)
    {
        Debug.LogWarning(gameObject.name + " has not implemented SetBoostBool() and is inheriting it from the Boost superclass!");
    }

    public void Apply()
    {
        BoostRemaining += Duration;
        if (BoostRemaining > MaxDuration)
        {
            BoostRemaining = MaxDuration;
        }
    }
}
