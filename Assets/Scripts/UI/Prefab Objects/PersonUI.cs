using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PersonUI : MonoBehaviour
{
    #region Data

    public GuildMember GuildMember { get; private set; }

    [SerializeField]
    private Sprite adventurerSprite;

    [SerializeField]
    private Sprite selectedArtisanSprite;

    [SerializeField]
    private Sprite artisanSprite;

    [SerializeField]
    private Sprite peasantSprite;

    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    [SerializeField]
    private TextMeshProUGUI personName;

    [SerializeField]
    private TextMeshProUGUI personVocation;

    [SerializeField]
    private TextMeshProUGUI health;

    [SerializeField]
    private TextMeshProUGUI availability;

    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private TextMeshProUGUI exp;

    [SerializeField]
    private Slider expSlider;

    [SerializeField]
    private TextMeshProUGUI quip;

    [SerializeField]
    private TextMeshProUGUI vocationStat;

    public GameObject beginBtn;
    public GameObject releaseBtn;
    public GameObject promoteToAdventurerBtn;
    public GameObject promoteToArtisanBtn;

    private QuestManager questManager;
    private bool isSelected;

    #endregion

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        ShowExtensionPanel();
        ShowExtensionPanel();
    }

    private void FixedUpdate()
    {
        if (GuildMember != null)
        {
            UpdateExpSlider();

            if (
                (GuildMember.Vocation is Peasant && GuildMember.Level == 10) ||
                (GuildMember.Vocation is Adventurer && GuildMember.Level == 20) ||
                (GuildMember.Vocation is Artisan && GuildMember.Level == 20)
                )
            {
                exp.text = "MAX";
                expSlider.value = expSlider.maxValue;
            }
            else
                exp.text = $"{GuildMember.Experience} / {Levelling.GuildMemberLevel[GuildMember.Level]}";

            personVocation.text = $"{GuildMember.Vocation.Title()} - Level {GuildMember.Level}";

            if (GuildMember.Vocation is Adventurer)
                health.text = $"HP: {GuildMember.Hitpoints} / {GuildMember.MaxHitpoints}";
            else
                health.text = "";

            if (GuildMember.IsAvailable && !GuildMember.IsIncapacitated)
                availability.text = "Idle";
            else if (GuildMember.IsAvailable && GuildMember.IsIncapacitated)
                availability.text = "Incapacitated";
            else
                availability.text = "Working";

            if (extensionPanel.activeInHierarchy)
                AdjustStatsPanel();
        }
    }

    public void SetPerson(GuildMember _person)
    {
        GuildMember = _person;
        personName.text = GuildMember.person.name;
        SetColor();
    }

    public void ClearPersonUI()
    {
        GuildMember = null;
        isSelected = false;
        SetColor();
        transform.SetParent(FindObjectOfType<PersonUIPool>().transform);
        beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Begin";
        if (extensionPanel.activeSelf == true)
            ShowExtensionPanel();

        gameObject.SetActive(false);
    }

    public void ShowExtensionPanel()
    {
        if (extensionPanel.activeSelf)
            extensionPanel.SetActive(false);
        else
        {
            extensionPanel.SetActive(true);
            if (GuildMember != null)
                avatarFrame.SetFrameAvatar(GuildMember);
        }
        AdjustStatsPanel();
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
    }

    public void HideExtensionPanel()
    {
        extensionPanel.SetActive(false);
    }

    public void Begin()
    {
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager.CurrentMenu == FindObjectOfType<Menu_SelectAdventurer>())
            BeginQuest();
        else if (menuManager.CurrentMenu == FindObjectOfType<Menu_SelectArtisans>())
            SelectArtisan();
        else if (menuManager.CurrentMenu == FindObjectOfType<Menu_SelectTrainee>())
            BeginTraining();
    }

    public void ShowBeginButton()
    {
        beginBtn.SetActive(true);
    }

    public void HideBeginButton()
    {
        beginBtn.SetActive(false);
    }

    public void ShowReleaseButton()
    {
        if (!GuildMember.gameObject.CompareTag("Hero") && GuildMember.IsAvailable)
            releaseBtn.SetActive(true);
        else
        {
            releaseBtn.SetActive(false);
        }
    }

    public void HideReleaseButton()
    {
        releaseBtn.SetActive(false);
    }

    public void ShowPromoteButtons()
    {
        if (GuildMember.Vocation is Peasant && GuildMember.Level >= 5)
        {
            promoteToAdventurerBtn.SetActive(true);
            promoteToArtisanBtn.SetActive(true);
            
            if (FindObjectOfType<PopulationManager>().AdventurersEnabled)
                promoteToAdventurerBtn.GetComponent<Button>().interactable = true;
            else
                promoteToAdventurerBtn.GetComponent<Button>().interactable = false;

            if (FindObjectOfType<PopulationManager>().ArtisansEnabled)
                promoteToArtisanBtn.GetComponent<Button>().interactable = true;
            else
                promoteToArtisanBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            HidePromoteButtons();
        }
    }

    public void HidePromoteButtons()
    {
        promoteToAdventurerBtn.SetActive(false);
        promoteToArtisanBtn.SetActive(false);
    }

    private void AdjustStatsPanel()
    {
        if (GuildMember != null)
        {
            quip.text = GuildMember.Quip;
            if (GuildMember.Vocation is Peasant peasant)
                vocationStat.text = $"{peasant.Income} {peasant.IncomeResource} per hour";
            else if (GuildMember.Vocation is Artisan artisan)
                vocationStat.text = $"Projects completed: {artisan.ProjectsCompleted}";
            else if (GuildMember.Vocation is Adventurer adventurer)
                vocationStat.text = $"Quests completed: {adventurer.QuestsCompleted}";
        }
    }

    public void SetColor()
    {
        foreach(GameObject obj in gameObject.GetChildren())
        {
            if (obj.name == "PersonUIPanel")
            {
                if (GuildMember == null) obj.GetComponent<Image>().sprite = peasantSprite;
                else if (isSelected) obj.GetComponent<Image>().sprite = selectedArtisanSprite;
                else if (GuildMember.Vocation is Artisan) obj.GetComponent<Image>().sprite = artisanSprite;
                else if (GuildMember.Vocation is Adventurer) obj.GetComponent<Image>().sprite = adventurerSprite;
                else obj.GetComponent<Image>().sprite = peasantSprite;
            }
        }
    }

    private void UpdateExpSlider()
    {
        if (GuildMember.Level == GuildMember.Vocation.MaxLevel)
        {
            expSlider.minValue = 0;
            expSlider.maxValue = Levelling.GuildMemberLevel[GuildMember.Level - 1];
        }
        else
        {
        expSlider.minValue = Levelling.GuildMemberLevel[GuildMember.Level - 1];
        expSlider.maxValue = Levelling.GuildMemberLevel[GuildMember.Level];
        }
        expSlider.value = GuildMember.Experience;
    }

    public void CallReleasePopup()
    {
        PopupManager popupManager = FindObjectOfType<PopupManager>();
        string description = $"Are you sure you wish to release {GuildMember.person.name} from the Adventure Guild? This cannot be undone.";
        popupManager.RequestGenericPopup("Release Guild Member", "", description, GuildMember.Avatar, "Release", "Cancel");
        popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(ConfirmRelease);
    }

    public void CallPromoteToPopup(string _vocation)
    {
        PopupManager popupManager = FindObjectOfType<PopupManager>();
        string description = $"Are you sure you wish to promote {GuildMember.person.name} to {_vocation}?";
        popupManager.RequestGenericPopup("Promote", "", description, GuildMember.Avatar, "Promote", "Cancel");
        if (_vocation == "Adventurer")
            popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(ConfirmPromoteAdventurer);
        else
            popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(ConfirmPromoteArtisan);
    }

    private void ConfirmRelease()
    {
        FindObjectOfType<PopupManager>().GenericPopup.ConfirmBtn.onClick.RemoveListener(ConfirmRelease);
        FindObjectOfType<PopulationManager>().RemoveGuildMember(GuildMember);
        GetComponentInParent<PersonUIScrollView>().LoadPersonUIs();
    }

    private void ConfirmPromoteAdventurer()
    {
        FindObjectOfType<PopupManager>().GenericPopup.ConfirmBtn.onClick.RemoveListener(ConfirmPromoteAdventurer);
        GuildMember.PromoteToAdventurer();
        FindObjectOfType<Menu_Hub>().Open();
    }

    private void ConfirmPromoteArtisan()
    {
        FindObjectOfType<PopupManager>().GenericPopup.ConfirmBtn.onClick.RemoveListener(ConfirmPromoteArtisan);
        GuildMember.PromoteToArtisan();
        FindObjectOfType<Menu_Hub>().Open();
    }

    private void BeginQuest()
    {
        questManager.SetAdventurer(GetComponent<PersonUI>().GuildMember);
        questManager.StartQuest();
        Menu_QuestJournal questJournal = FindObjectOfType<Menu_QuestJournal>();
        questJournal.SetQuest(questManager.CurrentQuest);
        questJournal.Open();
    }

    private void BeginTraining()
    {
        TrainingManager trainingManager = FindObjectOfType<TrainingManager>();
        trainingManager.SetGuildMember(GetComponent<PersonUI>().GuildMember);
        FindObjectOfType<Menu_Training>().Open();
        trainingManager.OpenInstructions();
    }

    private void SelectArtisan()
    {
        if (!isSelected)
        {
            isSelected = true;
            FindObjectOfType<ConstructionManager>().AddArtisan(GuildMember);
            beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Deselect";
        }
        else
        {
            isSelected = false;
            FindObjectOfType<ConstructionManager>().RemoveArtisan(GuildMember);
            beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }
        SetColor();
        extensionPanel.SetActive(false);
    }
}
