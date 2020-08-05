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
        Incident newIncident = new Incident();
        newIncident.Init();
        newIncident.description = incidents.GetRandomIncident().description;
        return newIncident;
    }

    public Incident GetIncident(DateTime _time)
    {
        Incident newIncident = new Incident();
        newIncident.time = _time.ToString();
        newIncident.description = incidents.GetRandomIncident().description;
        return newIncident;
    }

    public Incident CreateCustomIncident(string _description, Incident.Result _result, DateTime _time)
    {
        return new Incident() { description = _description, result = _result, time = _time.ToShortTimeString() };
    }
}
