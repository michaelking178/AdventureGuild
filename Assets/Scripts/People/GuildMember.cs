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
        MaxHitpoints = 50;
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

    public void AddExp(string expType, int _exp)
    {
        if (vocation.Title() != "Adventurer") return;

        Adventurer adv = (Adventurer)vocation;
        if (expType == "Combat")
        {
            adv.CombatExp += _exp;
        }
        else if (expType == "Espionage")
        {
            adv.EspionageExp += _exp;
        }
        else if (expType == "Diplomacy")
        {
            adv.DiplomacyExp += _exp;
        }
    }

    private void CheckLevel()
    {
        while (Experience > CharacterLevel.LevelValues[Level])
        {
            Level++;
            if (Vocation is Peasant && Level >= 5)
            {
                Vocation = new Adventurer();
                Experience = 0;
                Level = 1;
                MaxHitpoints = 100;
                FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} has honed their skills and become an Adventurer!", person.name), Notification.Type.GuildMember, Notification.Spirit.Good);
            }
            else
            {
                MaxHitpoints += 10;
                Hitpoints = MaxHitpoints;
                FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} reached Level {1}!", person.name, Level), Notification.Type.GuildMember, Notification.Spirit.Good);
            }
        }

    }
}
