using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private bool isTiming;

    private void Start()
    {
        currentTime = timeLimit;
    }

    public void SetTime(float _time)
    {
        timeLimit = _time;
    }

    private void FixedUpdate()
    {
        if (isTiming && currentTime >= 0.0f)
        {
            currentTime -= Time.fixedDeltaTime;
        }
        else
        {
            isTiming = false;
            currentTime = timeLimit;
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }
}
