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

    public GameObject contextBtn;
    public GameObject releaseBtn;
    public GameObject promoteToAdventurerBtn;
    public GameObject promoteToArtisanBtn;

    [Header("Adventurer Stats")]
    [SerializeField]
    private GameObject adventurerStats;

    [SerializeField]
    private TextMeshProUGUI combatExp;

    [SerializeField]
    private TextMeshProUGUI espionageExp;

    [SerializeField]
    private TextMeshProUGUI diplomacyExp;

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

    public void ShowContextButton()
    {
        if (contextBtn.activeSelf)
        {
            contextBtn.SetActive(false);
        }
        else
        {
            contextBtn.SetActive(true);
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
            combatExp.text = string.Format("Combat: {0} ({1}/{2})", adventurer.CombatLevel.ToString(), adventurer.CombatExp.ToString(), Levelling.SkillLevel[adventurer.CombatLevel].ToString());
            espionageExp.text = string.Format("Espionage: {0} ({1}/{2})", adventurer.EspionageLevel.ToString(), adventurer.EspionageExp.ToString(), Levelling.SkillLevel[adventurer.EspionageLevel].ToString());
            diplomacyExp.text = string.Format("Diplomacy: {0} ({1}/{2})", adventurer.DiplomacyLevel.ToString(), adventurer.DiplomacyExp.ToString(), Levelling.SkillLevel[adventurer.DiplomacyLevel].ToString());
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
