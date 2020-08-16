using UnityEngine;

public class PersonUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject personUI;

    private PopulationManager populationManager;

    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void GetAllGuildMembers()
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
        foreach (GuildMember guildMember in populationManager.GetAvailableAdventurers())
        {
            GameObject newPersonUI = Instantiate(personUI, transform);
            newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
            newPersonUI.GetComponent<PersonUI>().ShowButton();
        }
    }

    public void GetAvailableGuildMembers()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.IsAvailable)
            {
                GameObject newPersonUI = Instantiate(personUI, transform);
                newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
                if (!newPersonUI.GetComponent<PersonUI>().contextBtn.activeSelf)
                {
                    newPersonUI.GetComponent<PersonUI>().ShowButton();
                }
            }
        }
    }
}
