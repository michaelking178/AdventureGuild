using System;
using UnityEngine;

public class QuestTimer : MonoBehaviour
{
    [SerializeField]
    private Quest quest;

    public DateTime StartTime { get; set; }
    public float TimeLimit { get; set; }

    [SerializeField]
    public float CurrentTime { get; set; }

    public bool IsTiming { get; set; }
    public bool QuestFinished { get; set; } = false;
    public int IncidentTimer { get; set; } = 10;
    public int IncidentQueue { get; set; }

    private QuestManager questManager;
    private IncidentManager incidentManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        incidentManager = FindObjectOfType<IncidentManager>();
    }

    private void FixedUpdate()
    {
        if (IsTiming && CurrentTime < TimeLimit)
        {
            TimeSpan difference = DateTime.Now - StartTime;
            if (CurrentTime <= TimeLimit)
            {
                CurrentTime = (float)difference.TotalSeconds;
            }
            IncidentQueue = Mathf.FloorToInt((float)difference.TotalSeconds / IncidentTimer);
            if (quest.Incidents.Count < IncidentQueue)
            {
                for (int i = (IncidentQueue - quest.Incidents.Count); i > 0; i--)
                {
                    GenerateIncident(DateTime.Now.AddSeconds(-((i-1) * IncidentTimer)));
                }
            }
        }
        else if (quest.GuildMember != null)
        {
            EndQuest();
        }
        else
        {
            Debug.Log(string.Format("QuestTimer unable to complete quest \"{0}\" ({1})", quest.questName, quest.questInstanceId));
        }
    }

    public void StartTimer()
    {
        StartTime = DateTime.Now;
        IsTiming = true;
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
        TimeLimit = quest.time;
    }

    public Quest GetQuest()
    {
        return quest;
    }

    private void EndQuest()
    {
        IsTiming = false;
        if (!QuestFinished)
        {
            QuestFinished = true;
            questManager.CompleteQuest(quest);
            Destroy(gameObject);
        }
    }

    public void GenerateIncident(DateTime _time)
    {
        if (_time < (StartTime.AddSeconds(TimeLimit - 3.0f)))
        {
            quest.Incidents.Add(incidentManager.GetIncident(_time));
        }
    }
}
