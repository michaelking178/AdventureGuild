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
    private GameObject statsPanel;

    [SerializeField]
    private TextMeshProUGUI exp;

    [SerializeField]
    private Slider expSlider;

    public GameObject beginBtn;
    public GameObject releaseBtn;
    public GameObject promoteToAdventurerBtn;
    public GameObject promoteToArtisanBtn;

    [Header("Adventurer Stats")]
    [SerializeField]
    private GameObject adventurerStats;

    [SerializeField]
    private TextMeshProUGUI combatLevel;

    [SerializeField]
    private TextMeshProUGUI espionageLevel;

    [SerializeField]
    private TextMeshProUGUI diplomacyLevel;

    [SerializeField]
    private Slider combatSlider;

    [SerializeField]
    private Slider espionageSlider;

    [SerializeField]
    private Slider diplomacySlider;

    [SerializeField]
    private TextMeshProUGUI combatExpText;

    [SerializeField]
    private TextMeshProUGUI espionageExpText;

    [SerializeField]
    private TextMeshProUGUI diplomacyExpText;

    [Header("Peasant Stats")]
    [SerializeField]
    private GameObject peasantStats;

    [SerializeField]
    private TextMeshProUGUI income;

    [Header("Artisan Stats")]
    [SerializeField]
    private GameObject artisanStats;

    private QuestManager questManager;
    private bool isSelected;
    private Color defaultAdventurerColor = new Color(0.6f, 1, 0.7f);
    private Color defaultArtisanColor = new Color(0.3f, 0.7f, 1f);
    private Color selectedArtisanColor = new Color(0.25f, 0.4f, 0.73f);

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

            if (GuildMember.Vocation is Peasant && GuildMember.Level == 10)
            {
                exp.text = "MAX";
                expSlider.value = expSlider.maxValue;
            }
            else
                exp.text = $"{GuildMember.Experience} / {Levelling.GuildMemberLevel[GuildMember.Level]}";

            personVocation.text = $"{GuildMember.Vocation.Title()} - Level {GuildMember.Level}";
            health.text = $"HP: {GuildMember.Hitpoints} / {GuildMember.MaxHitpoints}";

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
    }

    public void HideReleaseButton()
    {
        releaseBtn.SetActive(false);
    }

    public void ShowPromoteButtons()
    {
        if (GuildMember.Vocation is Peasant && GuildMember.Level >= 5)
        {
            if (FindObjectOfType<PopulationManager>().AdventurersEnabled)
                promoteToAdventurerBtn.SetActive(true);

            if (FindObjectOfType<PopulationManager>().ArtisansEnabled)
                promoteToArtisanBtn.SetActive(true);
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
            if (GuildMember.Vocation is Peasant peasant)
            {
                adventurerStats.SetActive(false);
                artisanStats.SetActive(false);
                peasantStats.SetActive(true);
                Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
                statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
                income.text = $"{peasant.IncomeResource}: {peasant.Income}";
            }
            else if (GuildMember.Vocation is Artisan)
            {
                adventurerStats.SetActive(false);
                peasantStats.SetActive(false);
                artisanStats.SetActive(true);
                Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
                statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
            }
            else if (GuildMember.Vocation is Adventurer adventurer)
            {
                adventurerStats.SetActive(true);
                peasantStats.SetActive(false);
                artisanStats.SetActive(false);
                Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
                statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 215.0f);

                if (!questManager.CombatUnlocked)
                {
                    combatExpText.gameObject.SetActive(false);
                    combatSlider.gameObject.SetActive(false);
                    combatLevel.text = "Combat: Locked";
                }
                else
                {
                    combatExpText.gameObject.SetActive(true);
                    combatSlider.gameObject.SetActive(true);
                    combatLevel.text = $"Combat: {adventurer.CombatLevel}";
                    combatExpText.text = $"{adventurer.CombatExp} / {Levelling.SkillLevel[adventurer.CombatLevel]}";
                    combatSlider.minValue = Levelling.SkillLevel[adventurer.CombatLevel - 1];
                    combatSlider.maxValue = Levelling.SkillLevel[adventurer.CombatLevel];
                    combatSlider.value = adventurer.CombatExp;
                }

                if (!questManager.EspionageUnlocked)
                {
                    espionageExpText.gameObject.SetActive(false);
                    espionageSlider.gameObject.SetActive(false);
                    espionageLevel.text = "Espionage: Locked";
                }
                else
                {
                    espionageExpText.gameObject.SetActive(true);
                    espionageSlider.gameObject.SetActive(true);
                    espionageLevel.text = $"Espionage: {adventurer.EspionageLevel}";
                    espionageExpText.text = $"{adventurer.EspionageExp} / {Levelling.SkillLevel[adventurer.EspionageLevel]}";
                    espionageSlider.minValue = Levelling.SkillLevel[adventurer.EspionageLevel - 1];
                    espionageSlider.maxValue = Levelling.SkillLevel[adventurer.EspionageLevel];
                    espionageSlider.value = adventurer.EspionageExp;
                }
            
                if (!questManager.DiplomacyUnlocked)
                {
                    diplomacyExpText.gameObject.SetActive(false);
                    diplomacySlider.gameObject.SetActive(false);
                    diplomacyLevel.text = "Diplomacy: Locked";
                }
                else
                {
                    diplomacyExpText.gameObject.SetActive(true);
                    diplomacySlider.gameObject.SetActive(true);
                    diplomacyLevel.text = $"Diplomacy: {adventurer.DiplomacyLevel}";
                    diplomacyExpText.text = $"{adventurer.DiplomacyExp} / {Levelling.SkillLevel[adventurer.DiplomacyLevel]}";
                    diplomacySlider.minValue = Levelling.SkillLevel[adventurer.DiplomacyLevel - 1];
                    diplomacySlider.maxValue = Levelling.SkillLevel[adventurer.DiplomacyLevel];
                    diplomacySlider.value = adventurer.DiplomacyExp;
                }
            }
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
        expSlider.minValue = Levelling.GuildMemberLevel[GuildMember.Level - 1];
        expSlider.maxValue = Levelling.GuildMemberLevel[GuildMember.Level];
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
            beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Deselect Artisan";
        }
        else
        {
            isSelected = false;
            FindObjectOfType<ConstructionManager>().RemoveArtisan(GuildMember);
            beginBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Select Artisan";
        }
        SetColor();
        extensionPanel.SetActive(false);
    }
}
