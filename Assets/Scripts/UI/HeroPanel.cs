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

    private Person hero;

    public void UpdateHeroPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>();
        heroName.text = hero.GetName();
        heroVocation.text = string.Format("Level {0} {1}", hero.GetLevel(), hero.GetVocation().Title());
        heroExperience.text = "Experience: " + hero.GetExp().ToString();
        heroHealth.text = "Health: " + hero.GetHealth().ToString();
    }
}
