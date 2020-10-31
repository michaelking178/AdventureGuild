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
    public int DefaultIncidentTimer { get; set; } = 10;
    public int IncidentTimer { get; set; }
    public int IncidentQueue { get; set; }

    private QuestManager questManager;
    private IncidentManager incidentManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        incidentManager = FindObjectOfType<IncidentManager>();
        if (IncidentTimer == 0)
        {
            Debug.Log("Quest level: " + quest.level.ToString());
            IncidentTimer = Mathf.CeilToInt((quest.level / 3) + 1) * CreateIncidentTime();
            Debug.Log(IncidentTimer.ToString());
        }
    }

    private void FixedUpdate()
    {
        if (IsTiming && CurrentTime <= TimeLimit)
        {
            TimeSpan difference = DateTime.Now - StartTime;
            CurrentTime = (float)difference.TotalSeconds;
            IncidentQueue = Mathf.FloorToInt(CurrentTime / IncidentTimer);
            if (quest.Incidents.Count < IncidentQueue)
            {
                for (int i = (IncidentQueue - quest.Incidents.Count); i > 0; i--)
                {
                    GenerateIncident(DateTime.Now.AddSeconds(-(i-1) * IncidentTimer));
                    CheckHealth();
                    if (quest.GuildMember.Hitpoints == 0)
                    {
                        i = 1;
                    }
                }
            }
        }
        else if (quest.GuildMember != null)
        {
            CompleteQuest();
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

    private void CompleteQuest()
    {
        IsTiming = false;
        if (!QuestFinished)
        {
            QuestFinished = true;
            questManager.CompleteQuest(quest);
            Destroy(gameObject);
        }
    }

    private void FailQuest()
    {
        IsTiming = false;
        if (!QuestFinished)
        {
            QuestFinished = true;
            questManager.FailQuest(quest);
            Destroy(gameObject);
        }
    }

    public void GenerateIncident(DateTime _time)
    {
        if (_time < (StartTime.AddSeconds(TimeLimit - 3.0f)))
        {
            Incident incident = incidentManager.GetIncident(_time, quest.level, quest.GuildMember.Level);
            quest.Incidents.Add(incident);
            ApplyIncidentReward(incident);
        }
    }
    
    private void ApplyIncidentReward(Incident incident)
    {
        if (incident.reward != null)
        {
            FindObjectOfType<Guildhall>().AdjustGold(incident.reward.Gold);
            FindObjectOfType<Guildhall>().AdjustIron(incident.reward.Iron);
            FindObjectOfType<Guildhall>().AdjustWood(incident.reward.Wood);
            quest.GuildMember.AddExp(incident.reward.Experience);
            quest.GuildMember.AdjustHitpoints(incident.reward.Hitpoints);
        }
    }

    private void CheckHealth()
    {
        if (quest.GuildMember.Hitpoints <= 0)
        {
            FailQuest();
        }
    }

    private int CreateIncidentTime()
    {
        return Mathf.CeilToInt(DefaultIncidentTimer * UnityEngine.Random.Range(0.75f, 1.25f));
    }
}
