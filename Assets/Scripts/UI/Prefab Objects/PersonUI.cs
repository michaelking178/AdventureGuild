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
    private GameObject selectAdventurerBtn;

    [SerializeField]
    private TextMeshProUGUI personName;

    [SerializeField]
    private TextMeshProUGUI personVocation;

    [SerializeField]
    private TextMeshProUGUI availability;

    public void SetPerson(GuildMember _person)
    {
        guildMember = _person;
        SetPersonUIAttributes();
    }

    private void SetPersonUIAttributes()
    {
        personName.text = guildMember.person.name;
        personVocation.text = string.Format("{0} - Level {1}", guildMember.Vocation.Title(), guildMember.Level.ToString());

        if (guildMember.IsAvailable)
        {
            availability.text = "Available";
        }
        else
        {
            availability.text = "Unavailable";
        }
    }

    public void ShowPanel()
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

    public void ShowSelectButton()
    {
        if (selectAdventurerBtn.activeSelf)
        {
            selectAdventurerBtn.SetActive(false);
        }
        else
        {
            selectAdventurerBtn.SetActive(true);
        }
    }
}
