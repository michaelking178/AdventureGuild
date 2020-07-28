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
    private TextMeshProUGUI questExperience;

    [SerializeField]
    private TextMeshProUGUI questReward;

    [SerializeField]
    private TextMeshProUGUI incidentTextPrefab;

    [SerializeField]
    private GameObject incidentContainer;

    private QuestManager questManager;
    private Quest quest;
    private int incidentCount;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
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
        questName.text = quest.questName;
        summary.text = string.Format("{0}\nI, {1}, hereby set out to {2}. Success shall bring these rewards to the Guild:", quest.startTime.ToString(), quest.GuildMember.person.name, quest.commencement);
        questExperience.text = string.Format("{0} Experience", quest.Reward.Exp.ToString());
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
            incidentText.text = string.Format("{0}\n{1}", incident.time, incident.description);
        }
        incidentCount = quest.Incidents.Count;
    }
}
