using UnityEngine;

public class GuildMember : MonoBehaviour
{
    [SerializeField]
    public Person person;

    private Vocation vocation;
    public Vocation Vocation
    { 
        get
        {
            return vocation;
            
        }
        set
        {
            if (Vocation == null || Vocation.Title() == "Peasant")
            {
                vocation = value;
            }
            else
            {
                Debug.Log("That person already has a vocation!");
            }
        }
    }
    public int Id { get; set; }
    public int Health { get; set; }
    public Sprite Avatar { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
    public bool IsAvailable { get; set; }

    public void Init(Person _person)
    {
        person = _person;
        Id = Helpers.GenerateId();
        Health = 100;
        Experience = 0;
        Level = 1;
        Vocation = new Peasant();
        IsAvailable = true;
    }

    public void UpdateHealth(int change)
    {
        Health += change;
    }

    public void AddExp(int _exp)
    {
        Experience += _exp;
    }

    public void IncreaseLevel()
    {
        Level++;
    }
}
