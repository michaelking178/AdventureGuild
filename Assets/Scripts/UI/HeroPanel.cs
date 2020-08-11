using TMPro;
using UnityEngine;

public class HeroPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI heroName;

    [SerializeField]
    private TextMeshProUGUI heroVocation;

    [SerializeField]
    private TextMeshProUGUI heroHealth;

    [SerializeField]
    private TextMeshProUGUI heroExperience;

    private GuildMember hero;

    public void UpdateHeroPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        heroName.text = hero.person.name;
        heroVocation.text = string.Format("Level {0} {1}", hero.Level, hero.Vocation.Title());
        heroExperience.text = "Experience: " + hero.Experience.ToString();
        heroHealth.text = string.Format("Health: {0}/{1}", hero.Hitpoints.ToString(), hero.MaxHitpoints.ToString());
    }
}
