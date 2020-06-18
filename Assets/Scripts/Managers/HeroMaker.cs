using TMPro;
using UnityEngine;

public class HeroMaker : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultHeroSprite;

    private Person hero;
    private PopulationManager populationManager;

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void CreateHero()
    {
        GameObject heroObj = GameObject.FindGameObjectWithTag("Hero");
        if (heroObj.GetComponent<Person>() == null)
        {
            heroObj.AddComponent<Person>();
        }
        hero = heroObj.GetComponent<Person>();
        hero.SetGender(Person.Gender.MALE);
        hero.SetAvatar(defaultHeroSprite);
        hero.SetVocation(new Adventurer());
    }

    public void SetHeroGender(string _gender)
    {
        Person.Gender newGender = (Person.Gender)System.Enum.Parse(typeof(Person.Gender), _gender);
        hero.SetGender(newGender);
    }

    public void SetHeroName()
    {
        TextMeshProUGUI[] textBoxes = GameObject.Find("in_HeroName").GetComponentsInChildren<TextMeshProUGUI>();
        hero.SetName(textBoxes[1].text);
    }
}
