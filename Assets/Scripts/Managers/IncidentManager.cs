using System;
using UnityEngine;

public class IncidentManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset incidentsJson;

    private Incidents incidents;

    private void Start()
    {
        incidents = JsonUtility.FromJson<Incidents>(incidentsJson.text);
    }

    public Incident GetIncident(DateTime _time, int questLevel, int adventurerLevel)
    {
        Incident newIncident = GenerateIncident(questLevel, adventurerLevel);
        newIncident.time = _time.ToString();
        return newIncident;
    }

    private Incident GenerateIncident(int questLevel, int adventurerLevel)
    {
        Incident newIncident = new Incident();
        Incident incidentToClone = incidents.GetRandomIncident();
        newIncident.description = incidentToClone.description;
        newIncident.goodResult = incidentToClone.goodResult;
        newIncident.badResult = incidentToClone.badResult;
        newIncident.neutralResult = incidentToClone.neutralResult;
        newIncident.Init(questLevel, adventurerLevel);
        return newIncident;
    }

    public Incident CreateCustomIncident(string _description, Incident.Result _result, string _rewardMessage, DateTime _time)
    {
        return new Incident() { description = _description, result = _result, rewardMessage = _rewardMessage, time = _time.ToString() };
    }

    public Incident CreateCustomIncident(string _description, Incident.Result _result, string _rewardMessage)
    {
        return new Incident() { description = _description, result = _result, rewardMessage = _rewardMessage, time = "" };
    }
}
