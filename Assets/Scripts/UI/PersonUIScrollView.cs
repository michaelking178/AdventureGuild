using UnityEngine;

public class PersonUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject personUI;

    private PopulationManager populationManager;

    // Start is called before the first frame update
    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        UpdatePersonList();
    }

    public void UpdatePersonList()
    {
        foreach(GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            GameObject newPersonUI = Instantiate(personUI, transform);
            newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
        }
    }

    public void GetAvailableAdventurers()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.GetVocation() is Adventurer && guildMember.IsAvailable())
            {
                GameObject newPersonUI = Instantiate(personUI, transform);
                newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
            }
        }
    }
}
