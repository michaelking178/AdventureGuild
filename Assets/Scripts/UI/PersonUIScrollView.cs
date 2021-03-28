using System.Collections.Generic;
using TMPro;
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
        populationManager.SortGuildMembersByLevel();
        ClearPersonUIs();

        if (AdventurerGroup != null)
            LoadAvailableAdventurerUIs();
        
        if (ArtisanGroup != null)
            LoadAvailableArtisanUIs();
        
        if (PeasantGroup != null)
            LoadAvailablePeasantUIs();
    }

    public void ClearPersonUIs()
    {
        foreach(PersonUI personUI in adventurerUIs)
        {
            personUI.ClearPersonUI();
        }
        foreach (PersonUI personUI in artisanUIs)
        {
            personUI.ClearPersonUI();
        }
        foreach (PersonUI personUI in peasantUIs)
        {
            personUI.ClearPersonUI();
        }
        adventurerUIs.Clear();
        artisanUIs.Clear();
        peasantUIs.Clear();
    }

    public void SetPersonUIButtons(bool showBeginButton, bool showReleaseButton, bool showPromoteButtons, string beginButtonText = "")
    {
        List<PersonUI> personUIList = new List<PersonUI>();
        personUIList.AddRange(adventurerUIs);
        personUIList.AddRange(artisanUIs);
        personUIList.AddRange(peasantUIs);

        foreach (PersonUI person in personUIList)
        {
            if (showBeginButton)
            {
                person.GetComponent<PersonUI>().ShowBeginButton();
                if (beginButtonText != "")
                {
                    person.GetComponent<PersonUI>().beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = beginButtonText;
                }
            }
            else
                person.GetComponent<PersonUI>().HideBeginButton();

            if (showReleaseButton)
                person.GetComponent<PersonUI>().ShowReleaseButton();
            else
                person.GetComponent<PersonUI>().HideReleaseButton();

            if (showPromoteButtons)
                person.GetComponent<PersonUI>().ShowPromoteButtons();
            else
                person.GetComponent<PersonUI>().HidePromoteButtons();
        }
    }

    private void LoadHeroPersonUI()
    {
        GuildMember hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        LoadAdventurerUI();
        personUI.SetPerson(hero);
    }

    private void LoadAdventurerUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.transform.SetParent(AdventurerGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        adventurerUIs.Add(personUI);
        personUI.gameObject.SetActive(true);
    }

    private void LoadArtisanUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.transform.SetParent(ArtisanGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        artisanUIs.Add(personUI);
        personUI.gameObject.SetActive(true);
    }

    private void LoadPeasantUI()
    {
        personUI = personUIPool.GetNextAvailablePersonUI();
        personUI.transform.SetParent(PeasantGroup.GetComponent<GuildmemberGroup>().ContentPanel.transform);
        peasantUIs.Add(personUI);
        personUI.gameObject.SetActive(true);
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
