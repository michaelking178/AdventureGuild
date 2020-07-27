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
    private IncidentManager incidentManager;
    private bool questFinished = false;
    private float incidentTimer;
    private float defaultIncidentTimer = 10.0f;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        incidentManager = FindObjectOfType<IncidentManager>();
        currentTime = timeLimit;
        incidentTimer = defaultIncidentTimer;
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
            incidentTimer -= Time.fixedDeltaTime;
            if (incidentTimer <= 0 && (currentTime + 1) > defaultIncidentTimer)
            {
                GenerateIncident();
                incidentTimer = defaultIncidentTimer;
            }
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
            Destroy(gameObject);
        }
    }

    public void GenerateIncident()
    {
        quest.Incidents.Add(incidentManager.GetIncident());
    }
}
