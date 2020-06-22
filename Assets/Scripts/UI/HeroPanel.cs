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
        heroVocation.text = string.Format("Level {0} {1}", hero.GetLevel(), hero.GetVocation().Title());
        heroExperience.text = "Experience: " + hero.GetExp().ToString();
        heroHealth.text = "Health: " + hero.GetHealth().ToString();
    }
}
