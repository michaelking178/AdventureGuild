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
    public void GetIncident()
    {
        Incident incident = incidents.GetRandomIncident();
        Debug.Log(incident.time + " - " + incident.description + " - " + incident.result);
    }
}
