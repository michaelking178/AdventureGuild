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

    private PopulationManager populationManager;
    private PersonUIPool personUIPool;
    private PersonUI personUI;

    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        personUIPool = FindObjectOfType<PersonUIPool>();
    }

    public void LoadPersonUIs()
    {
        populationManager.SortGuildMembersByLevel();
        ClearPersonUIs();

        if (AdventurerGroup != null)
        {
            LoadHeroPersonUI();
            LoadAdventurerUIs();
        }

        if (ArtisanGroup != null)
            LoadArtisanUIs();

        if (PeasantGroup != null)
            LoadPeasantUIs();
    }

    public void LoadAvailablePersonUIs()
    {
        Debug.Log("Loading Available PersonUIs...");

        populationManager.SortGuildMembersByLevel();
        ClearPersonUIs();

        if (AdventurerGroup != null)
            LoadAvailableAdventurerUIs();
        else
            Debug.Log("Cannot find Adventurer Group!");
        
        if (ArtisanGroup != null)
            LoadAvailableArtisanUIs();
        
        if (PeasantGroup != null)
            LoadAvailablePeasantUIs();
    }

    public void ClearPersonUIs()
    {
        foreach(PersonUI personUI in adventurerUIs)
        {
            personUI.ClearPerson();
        }
        foreach (PersonUI personUI in artisanUIs)
        {
            personUI.ClearPerson();
        }
        foreach (PersonUI personUI in peasantUIs)
        {
            personUI.ClearPerson();
        }
        adventurerUIs.Clear();
        artisanUIs.Clear();
        peasantUIs.Clear();
    }

    public void SetPersonUIButtons(bool showBeginButton, bool showReleaseButton, bool showPromoteButtons)
    {
        List<PersonUI> personUIList = new List<PersonUI>();
        personUIList.AddRange(adventurerUIs);
        personUIList.AddRange(artisanUIs);
        personUIList.AddRange(peasantUIs);

        foreach (PersonUI person in personUIList)
        {
            if (showBeginButton && !person.GetComponent<PersonUI>().beginBtn.activeSelf)
                person.GetComponent<PersonUI>().ShowBeginButton();

            if (showReleaseButton && !person.GetComponent<PersonUI>().releaseBtn.activeSelf)
                person.GetComponent<PersonUI>().ShowReleaseButton();

            if (showPromoteButtons && !person.GetComponent<PersonUI>().promoteToAdventurerBtn.activeSelf && !person.GetComponent<PersonUI>().promoteToArtisanBtn.activeSelf)
                person.GetComponent<PersonUI>().ShowPromoteButtons();
        }
    }

    private void LoadHeroPersonUI()
    {
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        LoadAdventurerUI();
        personUI.SetPerson(hero);
        adventurerUIs.Add(personUI);
    }

    private void LoadAdventurerUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.gameObject.SetActive(true);
        personUI.transform.SetParent(AdventurerGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        adventurerUIs.Add(personUI);
    }

    private void LoadArtisanUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.gameObject.SetActive(true);
        personUI.transform.SetParent(ArtisanGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        artisanUIs.Add(personUI);
    }

    private void LoadPeasantUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.gameObject.SetActive(true);
        personUI.transform.SetParent(PeasantGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        peasantUIs.Add(personUI);
    }

    private void LoadAdventurerUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Adventurers().Count; i++)
        {
            if (!populationManager.Adventurers()[i].CompareTag("Hero"))
            {
                LoadAdventurerUI();
                personUI.SetPerson(populationManager.Adventurers()[i]);
            }
        }
    }

    private void LoadArtisanUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Artisans().Count; i++)
        {
            LoadArtisanUI();
            personUI.SetPerson(populationManager.Artisans()[i]);
        }
    }

    private void LoadPeasantUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Peasants().Count; i++)
        {
            LoadPeasantUI();
            personUI.SetPerson(populationManager.Peasants()[i]);
        }
    }

    private void LoadAvailableAdventurerUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Adventurers().Count; i++)
        {
            if (populationManager.Adventurers()[i].IsAvailable)
            {
                LoadAdventurerUI();
                personUI.SetPerson(populationManager.Adventurers()[i]);
            }
        }
    }

    private void LoadAvailableArtisanUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Artisans().Count; i++)
        {
            if (populationManager.Artisans()[i].IsAvailable)
            {
                LoadArtisanUI();
                personUI.SetPerson(populationManager.Artisans()[i]);
            }
        }
    }

    private void LoadAvailablePeasantUIs()
    {
        populationManager.SortGuildMembersByLevel();
        for (int i = 0; i < populationManager.Peasants().Count; i++)
        {
            if (populationManager.Peasants()[i].IsAvailable)
            {
                LoadPeasantUI();
                personUI.SetPerson(populationManager.Peasants()[i]);
            }
        }
    }
}
