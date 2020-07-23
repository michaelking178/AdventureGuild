using UnityEngine;

public class QuestTimer : MonoBehaviour
{
    [SerializeField]
    private Quest quest;

    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private bool isTiming;

    private QuestManager questManager;
    private bool questFinished = false;

    private void Start()
    {
        currentTime = timeLimit;
        questManager = FindObjectOfType<QuestManager>();
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
            StopTimer();
            EndQuest();
        }
    }

    public void StartTimer()
    {
        if (timeLimit != 0)
        {
            isTiming = true;
        }
        else
        {
            Debug.Log("Quest Timer has no time limit!");
        }
    }

    public void StopTimer()
    {
        if (isTiming)
        {
            isTiming = false;
            ResetTimer();
        }
    }

    public void PauseTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        currentTime = timeLimit;
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        timeLimit = quest.time;
    }

    private void EndQuest()
    {
        if (!questFinished)
        {
            questManager.CompleteQuest(quest);
            questFinished = true;
        }
    }
}
