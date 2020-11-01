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

    // Manage People
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
                InstantiatePersonUI(guildMember, false, false, true, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan")
            {
                InstantiatePersonUI(guildMember, false, false, true, false);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant")
            {
                InstantiatePersonUI(guildMember, false, false, true, true);
            }
        }
    }

    // Quests
    public void GetAvailableAdventurers()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (GuildMember guildMember in populationManager.GetAvailableAdventurers())
        {
            InstantiatePersonUI(guildMember, true, false, false, false);
        }
    }

    // Training
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
                InstantiatePersonUI(guildMember, false, true, false, false);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, false, true, false, false);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, false, true, false, false);
            }
        }
    }

    private void InstantiatePersonUI(GuildMember guildMember, bool showQuestButton, bool showTrainingButton, bool showReleaseButton, bool showPromoteButtons)
    {
        GameObject newPersonUI = Instantiate(personUI, transform);
        newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
        if (showQuestButton && !newPersonUI.GetComponent<PersonUI>().questBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowQuestButton();
        }
        if (showTrainingButton && !newPersonUI.GetComponent<PersonUI>().trainingBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowTrainingButton();
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
