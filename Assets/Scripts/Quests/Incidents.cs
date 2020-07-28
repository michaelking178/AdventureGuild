using UnityEngine;

[System.Serializable]
public class Incidents
{
    public Incident[] incidents;

    public Incident GetRandomIncident()
    {
        Incident incident = incidents[Random.Range(0, incidents.Length)];
        incident.Init();
        return incident;
    }
}
