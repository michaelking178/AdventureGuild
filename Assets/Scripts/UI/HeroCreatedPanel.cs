using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroCreatedPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI welcomeTextbox;

    private Person hero;
    private string prefix;
    private string paragraph;
    // It is a pleasure to meet you, Adventurer. I have a sense that our town's fortunes have turned with your arrival! Now, let me keep you no longer. To the guildhall!

    public void UpdateHeroCreatedPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>();
        SetPrefix();
        paragraph = string.Format("It is a pleasure to meet you, {0}{1}. I have a sense that our town's fortunes have turned with your arrival! Now, let me keep you no longer. To the guildhall!", prefix, hero.GetName());
        welcomeTextbox.text = paragraph;
    }

    private void SetPrefix()
    {
        if (hero.GetGender() == Person.Gender.MALE)
        {
            prefix = "Sir ";
        }
        else
        {
            prefix = "Lady ";
        }
    }
}
