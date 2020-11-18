using System.Collections.Generic;
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
    public void GetAllPeopleUI()
    {
        populationManager.SortGuildMembersByLevel();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }

        List<GuildMember> guildMembers = new List<GuildMember>();
        guildMembers.Add(GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>());
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Adventurer" && !guildMember.gameObject.CompareTag("Hero"))
            {
                guildMembers.Add(guildMember);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Artisan")
            {
                guildMembers.Add(guildMember);
            }
        }
        foreach (GuildMember guildMember in populationManager.GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant")
            {
                guildMembers.Add(guildMember);
            }
        }
        foreach (GuildMember guildMember in guildMembers)
        {
            InstantiatePersonUI(guildMember, false, true, true);
        }
    }

    // Quests
    public void GetAvailableAdventurersUI()
    {
        populationManager.SortGuildMembersByLevel();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        List<GuildMember> guildMembers = new List<GuildMember>();

        if (GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>().IsAvailable)
            guildMembers.Add(GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>());

        foreach (GuildMember guildMember in populationManager.GetAvailableAdventurers())
        {
            if (!guildMember.gameObject.CompareTag("Hero"))
                guildMembers.Add(guildMember);
        }
        foreach (GuildMember gm in guildMembers)
        {
            InstantiatePersonUI(gm, true, false, false);
        }
    }

    // Construction
    public void GetAvailableArtisansUI()
    {
        populationManager.SortGuildMembersByLevel();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (GuildMember guildMember in populationManager.GetAvailableArtisans())
        {
            InstantiatePersonUI(guildMember, false, false, false);
        }
    }

    // Combat Training (Adventurers and Peasants)
    public void GetCombatTrainingPeopleUI()
    {
        populationManager.SortGuildMembersByLevel();
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }

        List<GuildMember> guildMembers = new List<GuildMember>();

        if (GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>().IsAvailable)
            guildMembers.Add(GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>());

        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GetAvailableAdventurers())
        {
            if (!guildMember.gameObject.CompareTag("Hero"))
            {
                guildMembers.Add(guildMember);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant" && guildMember.IsAvailable)
            {
                guildMembers.Add(guildMember);
            }
        }
        foreach (GuildMember gm in guildMembers)
        {
            InstantiatePersonUI(gm, true, false, false);
        }
    }

    private void InstantiatePersonUI(GuildMember guildMember, bool showBeginButton, bool showReleaseButton, bool showPromoteButtons)
    {
        GameObject newPersonUI = Instantiate(personUI, transform);
        newPersonUI.GetComponent<PersonUI>().SetPerson(guildMember);
        if (showBeginButton && !newPersonUI.GetComponent<PersonUI>().beginBtn.activeSelf)
        {
            newPersonUI.GetComponent<PersonUI>().ShowBeginButton();
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
