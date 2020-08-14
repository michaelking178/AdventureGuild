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
    public int Hitpoints { get; set; }
    public int MaxHitpoints { get; set; }
    public Sprite Avatar { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsIncapacitated { get; set; }
    public string bio { get; set; }

    public void Init(Person _person)
    {
        person = _person;
        Id = Helpers.GenerateId();
        MaxHitpoints = 100;
        Hitpoints = MaxHitpoints;
        Experience = 0;
        Level = 1;
        Vocation = new Peasant();
        IsAvailable = true;
        IsIncapacitated = false;
        bio = "";
    }

    public void AdjustHitpoints(int change)
    {
        Hitpoints += change;
        if (Hitpoints < 0)
        {
            Hitpoints = 0;
        }
        if (Hitpoints > MaxHitpoints)
        {
            Hitpoints = MaxHitpoints;
        }
        if (Hitpoints == 0)
        {
            IsIncapacitated = true;
        }
        else
        {
            IsIncapacitated = false;
        }
    }

    public void AddExp(int _exp)
    {
        Experience += _exp;
        CheckLevel();
    }

    private void CheckLevel()
    {
        while (Experience > CharacterLevel.LevelValues[Level])
        {
            Level++;
            FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} reached Level {1}!", person.name, Level), Notification.Type.GuildMember, Notification.Spirit.Good);
        }
    }
}
