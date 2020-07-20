using TMPro;
using UnityEngine;

public class PersonUI : MonoBehaviour
{
    private GuildMember guildMember;

    [SerializeField]
    private GameObject extensionPanel;

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
        personVocation.text = string.Format("{0} - Level {1}", guildMember.GetVocation().Title(), guildMember.GetLevel().ToString());
        
        if (guildMember.IsAvailabile())
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

    public void SetAdventurerToQuest()
    {
        FindObjectOfType<QuestManager>().SetAdventurer(guildMember);
    }
}
