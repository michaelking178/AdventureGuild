﻿using UnityEngine;

public class Boost : MonoBehaviour
{
    public string Name;
    public string Description;
    public float Duration = 7200;
    public float MaxDuration = 21600;
    public float BoostRemaining = 0;
    public float BoostValue = 0.2f;

    protected BoostManager boostManager;

    private bool boostEnded = false;
    
    protected void Start()
    {
        boostManager = FindObjectOfType<BoostManager>();
    }

    protected void FixedUpdate()
    {
        if (BoostRemaining > 0)
        {
            SetBoostBool(true);
            BoostRemaining -= Time.deltaTime;
            boostEnded = false;
        }
        else
        {
            BoostRemaining = 0;
            EndBoost();
        }
    }

    protected virtual void SetBoostBool(bool value)
    {
        Debug.LogWarning(gameObject.name + " has not implemented SetBoostBool() and is inheriting it from the Boost superclass!");
    }

    protected virtual bool GetBoostBool()
    {
        Debug.LogWarning(gameObject.name + " has not implemented GetBoostBool() and is inheriting it from the Boost superclass!");
        return false;
    }

    public virtual void Apply()
    {
        SetBoostBool(true);
        BoostRemaining += Duration;
        if (BoostRemaining > MaxDuration)
        {
            BoostRemaining = MaxDuration;
        }
        boostManager.SetBoosts();
    }

    private void EndBoost()
    {
        if(!boostEnded)
        {
            SetBoostBool(false);
            boostManager.SetBoosts();
            boostEnded = true;
        }
    }
}
