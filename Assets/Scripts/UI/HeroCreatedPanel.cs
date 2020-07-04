using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroCreatedPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI welcomeTextbox;

    private GuildMember hero;
    private string prefix;
    private string paragraph;

    public void UpdateHeroCreatedPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        SetPrefix();
        paragraph = string.Format("It is a pleasure to meet you, {0}{1}. I have a sense that our town's fortunes have turned with your arrival! Now, let me keep you no longer. To the guildhall!", prefix, hero.person.name);
        welcomeTextbox.text = paragraph;
    }

    private void SetPrefix()
    {
        if (hero.person.gender == "MALE")
        {
            prefix = "Sir ";
        }
        else
        {
            prefix = "Lady ";
        }
    }
}
