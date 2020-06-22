using TMPro;
using UnityEngine;

public class HeroMaker : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultHeroSprite;

    private GuildMember hero;
    private PopulationManager populationManager;

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void CreateHero()
    {
        GameObject heroObj = GameObject.FindGameObjectWithTag("Hero");
        if (heroObj.GetComponent<GuildMember>() == null)
        {
            heroObj.AddComponent<GuildMember>();
        }
        hero = heroObj.GetComponent<GuildMember>();
        hero.person = new Person {
            gender = "male"
        };
        hero.UpdateHealth(100);
        hero.IncreaseLevel();
        hero.SetAvatar(defaultHeroSprite);
        hero.SetVocation(new Adventurer());
        hero.IsAvailable(true);
        populationManager.GuildMembers.Add(hero);
    }

    public void SetHeroGender(string _gender)
    {
        hero.person.gender = _gender;
    }

    public void SetHeroName()
    {
        TextMeshProUGUI[] textBoxes = GameObject.Find("in_HeroName").GetComponentsInChildren<TextMeshProUGUI>();
        hero.person.name = textBoxes[1].text;
    }
}
