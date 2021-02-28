using System.Collections.Generic;
using UnityEngine;

public class PersonUIScrollView : MonoBehaviour
{
    public GameObject AdventurerGroup;

    public GameObject ArtisanGroup;

    public GameObject PeasantGroup;

    private List<PersonUI> adventurerUIs = new List<PersonUI>();
    private List<PersonUI> artisanUIs = new List<PersonUI>();
    private List<PersonUI> peasantUIs = new List<PersonUI>();

    [SerializeField]
    private GameObject personUI;

    private PopulationManager populationManager;
    private PersonUIPool personUIPool;

    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        personUIPool = FindObjectOfType<PersonUIPool>();
        GetAllPeopleUI();
    }

    public void GetAllPeopleUI()
    {
        populationManager.SortGuildMembersByLevel();
        ClearPersonUIs();

        if (AdventurerGroup != null)
            LoadAdventurerUIs();
        
        if (ArtisanGroup != null)
            LoadArtisanUIs();
        
        if (PeasantGroup != null)
            LoadPeasantUIs();
    }

    public void LoadAdventurerUIs()
    {
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        if (hero.IsAvailable)
        {
            PersonUI personUI = personUIPool.GetNextAvailablePersonUI();
            personUI.gameObject.SetActive(true);
            personUI.transform.SetParent(AdventurerGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
            personUI.SetPerson(hero);
            adventurerUIs.Add(personUI);
        }

        for (int i = 0; i < populationManager.Adventurers().Count; i++)
        {
            if (!populationManager.Adventurers()[i].CompareTag("Hero"))
            {
                PersonUI personUI = personUIPool.GetNextAvailablePersonUI();
                personUI.gameObject.SetActive(true);
                personUI.transform.SetParent(AdventurerGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
                personUI.SetPerson(populationManager.Adventurers()[i]);
                adventurerUIs.Add(personUI);
            }
        }
    }

    public void LoadArtisanUIs()
    {
        for (int i = 0; i < populationManager.Artisans().Count; i++)
        {
            PersonUI personUI = personUIPool.GetNextAvailablePersonUI();
            personUI.gameObject.SetActive(true);
            personUI.transform.SetParent(ArtisanGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
            personUI.SetPerson(populationManager.Artisans()[i]);
            artisanUIs.Add(personUI);
        }
    }

    public void LoadPeasantUIs()
    {
        for (int i = 0; i < populationManager.Peasants().Count; i++)
        {
            PersonUI personUI = personUIPool.GetNextAvailablePersonUI();
            personUI.gameObject.SetActive(true);
            personUI.transform.SetParent(PeasantGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
            personUI.SetPerson(populationManager.Peasants()[i]);
            peasantUIs.Add(personUI);
        }
    }

    public void ClearPersonUIs()
    {
        foreach(PersonUI personUI in adventurerUIs)
        {
            personUI.ClearPerson();
            adventurerUIs.Remove(personUI);
        }
        foreach (PersonUI personUI in artisanUIs)
        {
            personUI.ClearPerson();
            artisanUIs.Remove(personUI);
        }
        foreach (PersonUI personUI in peasantUIs)
        {
            personUI.ClearPerson();
            peasantUIs.Remove(personUI);
        }
    }























    // Quests
    public void GetAvailableAdventurersUI()
    {
        populationManager.SortGuildMembersByLevel();
        ClearLists();

        if (GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>().IsAvailable)
            InstantiatePersonUI(GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>(), AdventurerGroup, true, false, false);

        foreach (GuildMember guildMember in populationManager.GetAvailableAdventurers())
        {
            if (!guildMember.gameObject.CompareTag("Hero"))
                InstantiatePersonUI(guildMember, AdventurerGroup, true, false, false);
        }
    }

    // Construction
    public void GetAvailableArtisansUI()
    {
        populationManager.SortGuildMembersByLevel();
        ClearLists();

        foreach (GuildMember guildMember in populationManager.GetAvailableArtisans())
        {
            InstantiatePersonUI(guildMember, ArtisanGroup, false, false, false);
        }
    }

    // Combat Training (Adventurers and Peasants)
    public void GetCombatTrainingPeopleUI()
    {
        populationManager.SortGuildMembersByLevel();
        ClearLists();

        if (GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>().IsAvailable)
            InstantiatePersonUI(GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>(), AdventurerGroup, true, false, false);

        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GetAvailableAdventurers())
        {
            if (!guildMember.gameObject.CompareTag("Hero"))
            {
                InstantiatePersonUI(guildMember, AdventurerGroup, true, false, false);
            }
        }
        foreach (GuildMember guildMember in FindObjectOfType<PopulationManager>().GuildMembers)
        {
            if (guildMember.Vocation.Title() == "Peasant" && guildMember.IsAvailable)
            {
                InstantiatePersonUI(guildMember, PeasantGroup, true, false, false);
            }
        }
    }

    private void InstantiatePersonUI(GuildMember guildMember, GameObject group, bool showBeginButton, bool showReleaseButton, bool showPromoteButtons)
    {
        if (group != null)
        {
            GameObject newPersonUI = Instantiate(personUI, group.GetComponent<GuildmemberGroup>().ContentPanel.transform);
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

    private void ClearLists()
    {
        if (AdventurerGroup != null)
        {
            foreach (GameObject child in AdventurerGroup.GetComponent<GuildmemberGroup>().ContentPanel.GetChildren())
            {
                if (child.GetComponent<PersonUI>() != null)
                    Destroy(child);
            }
        }

        if (ArtisanGroup != null)
        {
            foreach (GameObject child in ArtisanGroup.GetComponent<GuildmemberGroup>().ContentPanel.GetChildren())
            {
                if (child.GetComponent<PersonUI>() != null)
                    Destroy(child);
            }
        }
        if (PeasantGroup != null)
        {
            foreach (GameObject child in PeasantGroup.GetComponent<GuildmemberGroup>().ContentPanel.GetChildren())
            {
                if (child.GetComponent<PersonUI>() != null)
                    Destroy(child);
            }
        }
    }
}
