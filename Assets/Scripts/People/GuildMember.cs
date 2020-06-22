using UnityEngine;

public class GuildMember : MonoBehaviour
{
    public Person person;

    [SerializeField]
    private Vocation vocation;

    [SerializeField]
    private int health;

    [SerializeField]
    private Sprite avatar;

    [SerializeField]
    private int experience;

    [SerializeField]
    private int level;

    [SerializeField]
    private bool isAvailable;

    public GuildMember(Person _person)
    {
        person = _person;
        health = 100;
        experience = 0;
        level = 1;
        vocation = new Peasant();
        isAvailable = true;
    }

    public Vocation GetVocation()
    {
        return vocation;
    }

    public void SetVocation(Vocation _vocation)
    {
        if (vocation == null || vocation.Title() == "Peasant")
        {
            vocation = _vocation;
        }
        else
        {
            Debug.Log("That person already has a vocation!");
        }
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

    public bool IsAvailabile()
    {
        return isAvailable;
    }

    public void IsAvailable(bool _isAvailable)
    {
        isAvailable = _isAvailable;
    }
}
