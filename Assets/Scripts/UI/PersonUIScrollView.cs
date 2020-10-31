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
                InstantiatePersonUI(guildMember, false, true, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan")
            {
                InstantiatePersonUI(guildMember, false, true, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant")
            {
                InstantiatePersonUI(guildMember, false, true, true);
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
            InstantiatePersonUI(guildMember, true, false, false);
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
                InstantiatePersonUI(guildMember, true, false, false);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, true, false, false);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, true, false, false);
            }
        }
    }

    private void InstantiatePersonUI(GuildMember guildMember, bool showContextButton, bool showReleaseButton, bool showPromoteButtons)
    {
        GameObject newPersonUI = Instantiate(personUI, transform);
        newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
        if (showContextButton && !newPersonUI.GetComponent<PersonUI>().contextBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowContextButton();
        }
        if (showReleaseButton && !newPersonUI.GetComponent<PersonUI>().releaseBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowReleaseButton();
        }
        if (showPromoteButtons && !newPersonUI.GetComponent<PersonUI>().promoteToAdventurerBtn.activeSelf && !newPersonUI.GetComponent<PersonUI>().promoteToArtisanBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowPromoteButtons();
        }
    }
}
