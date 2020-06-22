using TMPro;
using UnityEngine;

public class PersonUI : MonoBehaviour
{
    private GuildMember guildMember;

    [SerializeField]
    private GameObject extensionPanel;

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
        Debug.Log(name + " Clicked");
        if (extensionPanel.activeSelf)
        {
            extensionPanel.SetActive(false);
        }
        else
        {
            extensionPanel.SetActive(true);
        }
    }
}
