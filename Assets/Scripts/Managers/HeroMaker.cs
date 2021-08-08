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
        hero.person = new Person(1, "Adventure", "Hero");
        hero.Id = Helpers.GenerateId();
        hero.MaxHitpoints = 100;
        hero.Hitpoints = hero.MaxHitpoints;
        hero.Level = 1;
        hero.Avatar = defaultHeroSprite;
        hero.Vocation = new Adventurer();
        hero.IsAvailable = true;
        hero.Bio = "";
        hero.Quip = "Adventure awaits!";
        populationManager.GuildMembers.Add(hero);
    }

    public void SetHeroGender(string _gender)
    {
        hero.person.gender = _gender;
    }

    public void SetHeroName(string name)
    {
        hero.person.name = name;
        hero.Created = true;
    }
}
