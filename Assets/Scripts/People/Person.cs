using UnityEngine;

public class Person : MonoBehaviour
{
    public enum Gender { MALE, FEMALE }
    private PopulationManager populationManager;

    [SerializeField]
    private string personName;

    [SerializeField]
    private Vocation vocation;

    [SerializeField]
    private int health;

    [SerializeField]
    private Sprite avatar;

    [SerializeField]
    private Gender gender;

    [SerializeField]
    private int experience;

    [SerializeField]
    private int level;

    private void Awake()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        personName = "";
        health = 100;
        experience = 0;
        level = 1;
        vocation = new Peasant();
        populationManager.AddPerson(this);
    }

    public string GetName()
    {
        return personName;
    }

    public void SetName(string _name)
    {
        if (personName == "")
        {
            personName = _name;
        }
    }

    public Vocation GetVocation()
    {
        return vocation;
    }

    public void SetVocation(Vocation _vocation)
    {
        if (vocation.Title() == "Adventurer" || vocation.Title() == "Artisan")
        {
            Debug.Log("That person already has a vocation!");
        }
        else
        {
            vocation = _vocation;
        }
    }

    public Gender GetGender()
    {
        return gender;
    }

    public void SetGender(Gender _gender)
    {
        gender = _gender;
    }

    public Sprite GetAvatar()
    {
        return avatar;
    }

    public void SetAvatar(Sprite _avatar)
    {
        avatar = _avatar;
    }

    public int GetHealth()
    {
        return health;
    }

    public void UpdateHealth(int change)
    {
        health += change;
    }

    public int GetExp()
    {
        return experience;
    }

    public void AddExp(int _exp)
    {
        experience += _exp;
    }

    public int GetLevel()
    {
        return level;
    }

    public void IncreaseLevel()
    {
        level++;
    }
}
