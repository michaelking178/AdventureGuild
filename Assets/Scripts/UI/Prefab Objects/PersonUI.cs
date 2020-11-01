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

    public GameObject questBtn;
    public GameObject trainingBtn;
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

    private void Start()
    {
        AdjustStatsPanel();
        popupManager = FindObjectOfType<PopupManager>();
    }

    private void FixedUpdate()
    {
        if (GuildMember != null)
        {
            personName.text = GuildMember.person.name;

            if (GuildMember.Vocation is Peasant && GuildMember.Level == 10)
            {
                exp.text = "MAX";
            }
            else
            {
                exp.text = string.Format("{0} / {1}", GuildMember.Experience, Levelling.GuildMemberLevel[GuildMember.Level]);
            }
            personVocation.text = string.Format("{0} - Level {1}", GuildMember.Vocation.Title(), GuildMember.Level.ToString());
            health.text = string.Format("Health: {0}/{1}", GuildMember.Hitpoints, GuildMember.MaxHitpoints);

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

    public void ShowQuestButton()
    {
        if (questBtn.activeSelf)
        {
            questBtn.SetActive(false);
        }
        else
        {
            questBtn.SetActive(true);
        }
    }

    public void ShowTrainingButton()
    {
        if (trainingBtn.activeSelf)
        {
            trainingBtn.SetActive(false);
        }
        else
        {
            trainingBtn.SetActive(true);
        }
    }

    public void ShowReleaseButton()
    {
        if (GuildMember.gameObject.tag != "Hero" && GuildMember.IsAvailable)
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
        }
        if (GuildMember.Vocation is Peasant && GuildMember.Level >= 5)
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

    private void AdjustStatsPanel()
    {
        if (GuildMember.Vocation is Peasant)
        {
            adventurerStats.SetActive(false);
            peasantStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
            Peasant peasant = (Peasant)GuildMember.Vocation;
            income.text = string.Format("{0}: {1}", peasant.IncomeResource.ToString(), peasant.Income);
        }
        else if (GuildMember.Vocation is Artisan)
        {
            adventurerStats.SetActive(false);
            peasantStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 136.0f);
            Artisan artisan = (Artisan)GuildMember.Vocation;
            income.text = "None. Artisans will help to build the Adventure Guild.";
        }
        else if (GuildMember.Vocation is Adventurer adventurer)
        {
            adventurerStats.SetActive(true);
            peasantStats.SetActive(false);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 215.0f);
            combatLevel.text = $"Combat: {adventurer.CombatLevel}";
            espionageLevel.text = $"Espionage: {adventurer.EspionageLevel}";
            diplomacyLevel.text = $"Diplomacy: {adventurer.DiplomacyLevel}";

            combatExpText.text = $"{adventurer.CombatExp} / {Levelling.SkillLevel[adventurer.CombatLevel]}";
            espionageExpText.text = $"{adventurer.EspionageExp} / {Levelling.SkillLevel[adventurer.EspionageLevel]}";
            diplomacyExpText.text = $"{adventurer.DiplomacyExp} / {Levelling.SkillLevel[adventurer.DiplomacyLevel]}";
            UpdateExpSliders(adventurer);
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

    private void UpdateExpSliders(Adventurer adventurer)
    {
        combatSlider.minValue = Levelling.SkillLevel[adventurer.CombatLevel - 1];
        combatSlider.maxValue = Levelling.SkillLevel[adventurer.CombatLevel];
        combatSlider.value = adventurer.CombatExp;

        espionageSlider.minValue = Levelling.SkillLevel[adventurer.EspionageLevel - 1];
        espionageSlider.maxValue = Levelling.SkillLevel[adventurer.EspionageLevel];
        espionageSlider.value = adventurer.EspionageExp;

        diplomacySlider.minValue = Levelling.SkillLevel[adventurer.DiplomacyLevel - 1];
        diplomacySlider.maxValue = Levelling.SkillLevel[adventurer.DiplomacyLevel];
        diplomacySlider.value = adventurer.DiplomacyExp;
    }

    public void CallReleasePopup()
    {
        string description = string.Format("Are you sure you wish to release {0} from the Adventure Guild? This cannot be undone.", GuildMember.person.name);
        popupManager.CreateDefaultContent(description);
        popupManager.SetDoubleButton("Release", "Cancel");
        popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmRelease);

        popupManager.Populate("Release Guild Member", GuildMember.Avatar, gameObject);
        popupManager.CallPopup();
    }

    public void CallPromoteToPopup(string _vocation)
    {
        string description = string.Format("Are you sure you wish to promote {0} to {1}?", GuildMember.person.name, _vocation);
        popupManager.CreateDefaultContent(description);
        popupManager.SetDoubleButton("Promote", "Cancel");
        if (_vocation == "Adventurer") popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmPromoteAdventurer);
        else popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(ConfirmPromoteArtisan);

        popupManager.Populate("Promote", GuildMember.Avatar, gameObject);
        popupManager.CallPopup();
    }

    private void ConfirmRelease()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmRelease);
        FindObjectOfType<PopulationManager>().RemoveGuildMember(GuildMember);
        GetComponentInParent<PersonUIScrollView>().GetAllGuildMembers();
    }

    private void ConfirmPromoteAdventurer()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmPromoteAdventurer);
        FindObjectOfType<MenuManager>().OpenMenu("Menu_ManagePeople");
        GuildMember.PromoteToAdventurer();
    }

    private void ConfirmPromoteArtisan()
    {
        popupManager.Popup.GetComponentInChildren<Button>().onClick.RemoveListener(ConfirmPromoteArtisan);
        FindObjectOfType<MenuManager>().OpenMenu("Menu_ManagePeople");
        GuildMember.PromoteToArtisan();
    }
}
