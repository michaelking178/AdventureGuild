using UnityEngine;
using TMPro;

public class PersonUI : MonoBehaviour
{
    private GuildMember guildMember;
    public GuildMember GuildMember
    {
        get { return guildMember; }
    }

    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    [SerializeField]
    private GameObject beginQuestBtn;

    [SerializeField]
    private TextMeshProUGUI personName;

    [SerializeField]
    private TextMeshProUGUI personVocation;

    [SerializeField]
    private TextMeshProUGUI health;

    [SerializeField]
    private TextMeshProUGUI availability;

    private void FixedUpdate()
    {
        if (guildMember != null)
        {
            personName.text = guildMember.person.name;
            personVocation.text = string.Format("{0} - Level {1}", guildMember.Vocation.Title(), guildMember.Level.ToString());
            health.text = string.Format("Health: {0}/{1}", guildMember.Hitpoints, guildMember.MaxHitpoints);

            if (guildMember.IsAvailable && !guildMember.IsIncapacitated)
            {
                availability.text = "Available";
            }
            else if (guildMember.IsAvailable && guildMember.IsIncapacitated)
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
        guildMember = _person;
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
            avatarFrame.SetFrameAvatar(guildMember);
        }
    }

    public void ShowBeginQuestButton()
    {
        if (beginQuestBtn.activeSelf)
        {
            beginQuestBtn.SetActive(false);
        }
        else
        {
            beginQuestBtn.SetActive(true);
        }
    }
}
