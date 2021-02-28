﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_QuestJournal : Menu
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI questName;

    [SerializeField]
    private TextMeshProUGUI summary;

    [SerializeField]
    private TextMeshProUGUI questReward;

    [SerializeField]
    private TextMeshProUGUI incidentTextPrefab;

    [SerializeField]
    private TextMeshProUGUI incidentRewardTextPrefab;

    [SerializeField]
    private GameObject incidentContainer;

    [SerializeField]
    private Scrollbar scrollbar;

    private Quest quest;
    private int incidentCount;

    #endregion

    private void FixedUpdate()
    {
        if (quest != null && incidentCount != quest.Incidents.Count)
        {
            UpdateIncidents();
        }
    }

    public void SetQuest(Quest _quest)
    {
        quest = _quest;
    }

    public void UpdateQuestJournal()
    {
        if (quest == null)
        {
            Debug.LogError("Menu_QuestJournal has no quest assigned!");
            return;
        }
        string commencement = string.Format(quest.commencement, quest.contractor);
        questName.text = quest.questName;
        summary.text = $"{quest.startTime}\nI, {quest.GuildMember.person.name}, hereby set out to {commencement} Success shall reap these rewards:";
        questReward.text = Helpers.QuestRewardStr(quest);
        incidentCount = quest.Incidents.Count;
        UpdateIncidents();
        scrollbar.value = 1;
    }

    private void UpdateIncidents()
    {
        List<GameObject> incidentChildren = incidentContainer.GetChildren();
        foreach (GameObject child in incidentChildren)
        {
            Destroy(child);
        }
        foreach (Incident incident in quest.Incidents)
        {
            TextMeshProUGUI incidentText = Instantiate(incidentTextPrefab, incidentContainer.transform);
            incidentText.text = $"Day {quest.Incidents.IndexOf(incident) + 1}\n{incident.description} {incident.finalResult}";
            TextMeshProUGUI incidentRewardText = Instantiate(incidentRewardTextPrefab, incidentContainer.transform);
            switch (incident.result)
            {
                case Incident.Result.Good:
                    incidentRewardText.color = new Color(0.5254902f, 0.8313726f, 0.4666667f);
                    break;
                case Incident.Result.Bad:
                    incidentRewardText.color = new Color(1.0f, 0.3383238f, 0.2028302f);
                    break;
                case Incident.Result.Neutral:
                    incidentRewardText.color = Color.white;
                    break;
                default:
                    break;
            }
            if (Helpers.IncidentRewardStr(incident) != "")
            {
                incidentRewardText.text = Helpers.IncidentRewardStr(incident);
            }
            else if (incident.rewardMessage != "")
            {
                incidentRewardText.text = incident.rewardMessage;
            }
            else
            {
                incidentRewardText.color = new Color(1, 1, 1, 0);
                incidentRewardText.text = ".";
            }
        }
        incidentCount = quest.Incidents.Count;
    }
}
