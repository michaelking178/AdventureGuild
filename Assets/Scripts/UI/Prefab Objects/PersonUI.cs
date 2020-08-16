using UnityEngine;
using TMPro;

public class PersonUI : MonoBehaviour
{
    public GuildMember GuildMember { get; private set; }

    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private GameObject statsPanel;

    [SerializeField]
    private GameObject adventurerStats;

    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    [SerializeField]
    private TextMeshProUGUI exp;

    [SerializeField]
    private TextMeshProUGUI personName;

    [SerializeField]
    private TextMeshProUGUI personVocation;

    [SerializeField]
    private TextMeshProUGUI health;

    [SerializeField]
    private TextMeshProUGUI availability;

    public GameObject contextBtn;

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
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 103.0f);
        }
        else if (GuildMember.Vocation is Adventurer)
        {
            adventurerStats.SetActive(true);
            Vector2 rectSize = statsPanel.GetComponent<RectTransform>().rect.size;
            statsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x, 215.0f);
        }
    }
}
