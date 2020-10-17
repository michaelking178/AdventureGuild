using UnityEngine;
using TMPro;

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

    private void Start()
    {
        AdjustStatsPanel();
    }

    private void FixedUpdate()
    {
        if (GuildMember != null)
        {
            personName.text = GuildMember.person.name;
            exp.text = string.Format("{0} / {1}", GuildMember.Experience, CharacterLevel.LevelValues[GuildMember.Level]);
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

    public void ShowButton()
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

    private void AdjustStatsPanel()
    {
        if (GuildMember.Vocation is Peasant)
        {
            adventurerStats.SetActive(false);
            peasantStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 103.0f);
            Peasant peasant = (Peasant)GuildMember.Vocation;
            income.text = string.Format("{0}: {1}", peasant.IncomeResource.ToString(), peasant.Income);
        }
        else if (GuildMember.Vocation is Adventurer adventurer)
        {
            adventurerStats.SetActive(true);
            peasantStats.SetActive(false);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 215.0f);
            combatExp.text = "Combat: " + adventurer.CombatExp.ToString();
            espionageExp.text = "Espionage: " + adventurer.EspionageExp.ToString();
            diplomacyExp.text = "Diplomacy: " + adventurer.DiplomacyExp.ToString();
        }
    }
}
