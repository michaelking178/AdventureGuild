using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu_QuestJournal : MonoBehaviour
{
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

    private Quest quest;
    private int incidentCount;

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
        questName.text = quest.questName;
        summary.text = string.Format("{0}\nI, {1}, hereby set out to {2} Success shall reap these rewards:", quest.startTime.ToString(), quest.GuildMember.person.name, quest.commencement);
        questReward.text = Helpers.QuestRewardStr(quest);
        incidentCount = quest.Incidents.Count;
        UpdateIncidents();
    }

    private void FixedUpdate()
    {
        if (quest != null && incidentCount != quest.Incidents.Count)
        {
            UpdateIncidents();
        }
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
            incidentText.text = string.Format("{0}\n{1}", incident.time, incident.description + " " + incident.finalResult);
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
            if (incident.reward != null)
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
