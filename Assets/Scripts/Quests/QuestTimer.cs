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

    private LevelManager levelManager;
    private QuestManager questManager;
    private IncidentManager incidentManager;
    private Guildhall guildhall;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        questManager = FindObjectOfType<QuestManager>();
        incidentManager = FindObjectOfType<IncidentManager>();
        guildhall = FindObjectOfType<Guildhall>();
        if (IncidentTimer == 0)
        {
            IncidentTimer = Mathf.CeilToInt((quest.level / 3) + 1) * CreateIncidentTime();
        }
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentLevel() == "Title") return;

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
            Debug.Log($"QuestTimer unable to complete quest \"{quest.questName}\" ({quest.questInstanceId})");
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
            //Todo: ExpBoost debug can be removed later.
            if (FindObjectOfType<PopulationManager>().DebugBoostEnabled)
            {
                incident.reward.Gold *= FindObjectOfType<PopulationManager>().DebugBoost;
                incident.reward.Iron *= FindObjectOfType<PopulationManager>().DebugBoost;
                incident.reward.Wood *= FindObjectOfType<PopulationManager>().DebugBoost;
            }

            guildhall.AdjustGold(incident.reward.Gold);
            guildhall.AdjustIron(incident.reward.Iron);
            guildhall.AdjustWood(incident.reward.Wood);
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
