using System;
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
                Debug.Log(string.Format("{0} already has a vocation!", person.name));
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
    public bool Created { get; set; } = false;

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
        Created = true;
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

    public void AddExp(Quest.Skill skillType, int _exp)
    {
        if (vocation.Title() != "Adventurer") return;

        Adventurer adv = (Adventurer)vocation;
        if (skillType == Quest.Skill.Combat)
        {
            adv.CombatExp += _exp;
        }
        else if (skillType == Quest.Skill.Espionage)
        {
            adv.EspionageExp += _exp;
        }
        else if (skillType == Quest.Skill.Diplomacy)
        {
            adv.DiplomacyExp += _exp;
        }
    }

    private void CheckLevel()
    {
        if (Vocation is Peasant && Level == 10)
        {
            return;
        }
        while (Experience >= CharacterLevel.LevelValues[Level])
        {
            Level++;
            FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} reached Level {1}!", person.name, Level), Notification.Type.GuildMember, Notification.Spirit.Good);

            if (Vocation is Peasant peasant)
            {
                FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, -peasant.Income);
                peasant.Income = Mathf.CeilToInt(peasant.Income * 1.5f);
                FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, peasant.Income);
                if (Level == 5)
                {
                    FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} can now choose a vocation!", person.name, Level), Notification.Type.GuildMember, Notification.Spirit.Good);
                }
            }
            MaxHitpoints += 10;
            Hitpoints = MaxHitpoints;
        }
    }

    public void PromoteToAdventurer()
    {
        Peasant peasant = (Peasant)Vocation;
        FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Adventurer();
        Experience = 0;
        Level = 1;
        MaxHitpoints = 100;
        Hitpoints = MaxHitpoints;
        FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} has honed their skills and become an Adventurer!", person.name), Notification.Type.GuildMember, Notification.Spirit.Good);
    }

    public void PromoteToArtisan()
    {
        Peasant peasant = (Peasant)Vocation;
        FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Artisan();
        Experience = 0;
        Level = 1;
        MaxHitpoints = 100;
        Hitpoints = MaxHitpoints;
        FindObjectOfType<NotificationManager>().CreateNotification(string.Format("{0} has honed their skills and become an Artisan!", person.name), Notification.Type.GuildMember, Notification.Spirit.Good);
    }
}
