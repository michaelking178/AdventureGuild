using System;
using UnityEngine;

public class IncidentManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset incidentsJson;

    public bool GrabIncident = false;

    private Incidents incidents;

    private void Start()
    {
        incidents = JsonUtility.FromJson<Incidents>(incidentsJson.text);
    }

    private void Update()
    {
        if (GrabIncident)
        {
            GetIncident();
            GrabIncident = false;
        }
    }
    
    public Incident GetIncident()
    {
        return GenerateIncident();
    }

    public Incident GetIncident(DateTime _time)
    {
        Incident newIncident = GenerateIncident();
        newIncident.time = _time.ToString();
        return newIncident;
    }

    private Incident GenerateIncident()
    {
        Incident newIncident = new Incident();
        Incident incidentToClone = incidents.GetRandomIncident();
        newIncident.description = incidentToClone.description;
        newIncident.goodResult = incidentToClone.goodResult;
        newIncident.badResult = incidentToClone.badResult;
        newIncident.neutralResult = incidentToClone.neutralResult;
        newIncident.Init();
        return newIncident;
    }

    public Incident CreateCustomIncident(string _description, Incident.Result _result, string _rewardMessage, DateTime _time)
    {
        return new Incident() { description = _description, result = _result, rewardMessage = _rewardMessage, time = _time.ToString() };
    }
}
