using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonUI : MonoBehaviour
{
    private Person person;

    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private TextMeshProUGUI personName;

    [SerializeField]
    private TextMeshProUGUI personVocation;

    [SerializeField]
    private TextMeshProUGUI availability;

    public void SetPerson(Person _person)
    {
        person = _person;
        SetPersonUIAttributes();
    }

    private void SetPersonUIAttributes()
    {
        personName.text = person.GetName();
        personVocation.text = string.Format("{0} - Level {1}", person.GetVocation().Title(), person.GetLevel().ToString());
        
        if (person.IsAvailabile())
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
