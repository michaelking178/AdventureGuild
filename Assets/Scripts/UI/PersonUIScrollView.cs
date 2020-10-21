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
            if (guildMember.Vocation.Title() == "Adventurer")
            {
                InstantiatePersonUI(guildMember, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan")
            {
                InstantiatePersonUI(guildMember, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant")
            {
                InstantiatePersonUI(guildMember, false);
            }
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
            InstantiatePersonUI(guildMember, true);
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
            if (guildMember.Vocation.Title() == "Adventurer" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, true);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, true);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, true);
            }
        }
    }

    private void InstantiatePersonUI(GuildMember guildMember, bool showButton)
    {
        GameObject newPersonUI = Instantiate(personUI, transform);
        newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
        if (showButton && !newPersonUI.GetComponent<PersonUI>().contextBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowButton();
        }
    }
}
