using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PersonUI : MonoBehaviour
{
    public GuildMember GuildMember { get; private set; }

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

    private PopupManager popupManager;
    private QuestManager questManager;

    private void Start()
    {
        popupManager = FindObjectOfType<PopupManager>();
        questManager = FindObjectOfType<QuestManager>();
        AdjustStatsPanel();
    }

    private void FixedUpdate()
    {
        if (GuildMember != null)
        {
            personName.text = GuildMember.person.name;
            UpdateExpSlider();

            if (GuildMember.Vocation is Peasant && GuildMember.Level == 10)
            {
                exp.text = "MAX";
            }
            else
            {
                exp.text = $"{GuildMember.Experience} / {Levelling.GuildMemberLevel[GuildMember.Level]}";
            }
            personVocation.text = $"{GuildMember.Vocation.Title()} - Level {GuildMember.Level}";
            health.text = $"Health: {GuildMember.Hitpoints} / {GuildMember.MaxHitpoints}";

            if (GuildMember.IsAvailable && !GuildMember.IsIncapacitated)
            {
                availability.text = "Available";
            }
            else if (GuildMember.IsAvailable && GuildMember.IsIncapacitated)
            {
                availability.text = "Incapacitated";
            }
            else
            {
                availability.text = "Unavailable";
            }

            if (extensionPanel.activeInHierarchy)
            {
                AdjustStatsPanel();
            }
        }
    }

    public void SetPerson(GuildMember _person)
    {
        GuildMember = _person;
        SetColor();
    }

    public void ShowExtensionPanel()
    {
        if (extensionPanel.activeSelf)
        {
            extensionPanel.SetActive(false);
        }
        else
        {
            extensionPanel.SetActive(true);
            avatarFrame.SetFrameAvatar(GuildMember);
        }
    }

    public void ShowBeginButton()
    {
        if (beginBtn.activeSelf)
        {
            beginBtn.SetActive(false);
        }
        else
        {
            beginBtn.SetActive(true);
        }
    }

    public void ShowReleaseButton()
    {
        if (!GuildMember.gameObject.CompareTag("Hero") && GuildMember.IsAvailable)
        {
            if (releaseBtn.activeSelf)
            {
                releaseBtn.SetActive(false);
            }
            else
            {
                releaseBtn.SetActive(true);
            }
        }
    }

    public void ShowPromoteButtons()
    {
        if (GuildMember.Vocation is Peasant && GuildMember.Level >= 5)
        {
            if (promoteToAdventurerBtn.activeSelf)
            {
                promoteToAdventurerBtn.SetActive(false);
            }
            else
            {
                promoteToAdventurerBtn.SetActive(true);
            }
            if (FindObjectOfType<PopulationManager>().ArtisansEnabled)
            {
                if (promoteToArtisanBtn.activeSelf)
                {
                    promoteToArtisanBtn.SetActive(false);
                }
                else
                {
                    promoteToArtisanBtn.SetActive(true);
                }
            }
        }
    }

    private void AdjustStatsPanel()
    {
        if (GuildMember.Vocation is Peasant peasant)
        {
            adventurerStats.SetActive(false);
            peasantStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
            income.text = $"{peasant.IncomeResource}: {peasant.Income}";
        }
        else if (GuildMember.Vocation is Artisan)
        {
            adventurerStats.SetActive(false);
            peasantStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
            income.text = "None. Artisans will help to build the Adventure Guild.";
        }
        else if (GuildMember.Vocation is Adventurer adventurer)
        {
            adventurerStats.SetActive(true);
            peasantStats.SetActive(false);
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

    public void SetColor()
    {
        foreach(GameObject obj in Helpers.GetChildren(gameObject))
        {
            if (obj.name == "PersonUIPanel")
            {
                if (GuildMember.Vocation is Adventurer) obj.GetComponent<Image>().color = new Color(0.6f, 1, 0.7f);
                else if (GuildMember.Vocation is Artisan) obj.GetComponent<Image>().color = new Color(0.3f, 0.7f, 1f);
                else obj.GetComponent<Image>().color = new Color(1, 1, 1);
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
        string description = $"Are you sure you wish to release {GuildMember.person.name} from the Adventure Guild? This cannot be undone.";
        popupManager.CreateDefaultContent(description);
        popupManager.SetDoubleButton("Release", "Cancel");
        popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmRelease);

        popupManager.Populate("Release Guild Member", GuildMember.Avatar);
        popupManager.CallPopup();
    }

    public void CallPromoteToPopup(string _vocation)
    {
        string description = $"Are you sure you wish to promote {GuildMember.person.name} to {_vocation}?";
        popupManager.CreateDefaultContent(description);
        popupManager.SetDoubleButton("Promote", "Cancel");
        if (_vocation == "Adventurer") popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmPromoteAdventurer);
        else popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmPromoteArtisan);

        popupManager.Populate("Promote", GuildMember.Avatar);
        popupManager.CallPopup();
    }

    private void ConfirmRelease()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmRelease);
        FindObjectOfType<PopulationManager>().RemoveGuildMember(GuildMember);
        GetComponentInParent<PersonUIScrollView>().GetAllPeopleUI();
    }

    private void ConfirmPromoteAdventurer()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmPromoteAdventurer);
        FindObjectOfType<MenuManager>().OpenMenu("Menu_Hub");
        GuildMember.PromoteToAdventurer();
    }

    private void ConfirmPromoteArtisan()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmPromoteArtisan);
        FindObjectOfType<MenuManager>().OpenMenu("Menu_Hub");
        GuildMember.PromoteToArtisan();
    }
}
