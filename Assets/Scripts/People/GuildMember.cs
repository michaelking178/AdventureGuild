using UnityEngine;

public class GuildMember : MonoBehaviour
{
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
                Debug.Log($"{person.name} already has a vocation!");
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
    public string Bio { get; set; }
    public bool Created { get; set; } = false;

    private Guildhall guildhall;
    private NotificationManager notificationManager;

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
        Bio = "";
        Created = true;
    }

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        notificationManager = FindObjectOfType<NotificationManager>();
        CheckSkillLevels();
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
        // Todo: DebugBoost testing tool can be removed later.
        PopulationManager populationManager = FindObjectOfType<PopulationManager>();
        if (populationManager.DebugBoostEnabled) _exp *= populationManager.DebugBoost;

        Experience += _exp;
        CheckLevel();
    }

    public void AddExp(Quest.Skill skillType, int _exp)
    {
        // Todo: DebugBoost testing tool can be removed later.
        PopulationManager populationManager = FindObjectOfType<PopulationManager>();
        if (populationManager.DebugBoostEnabled) _exp *= populationManager.DebugBoost;

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
        CheckSkillLevels();
    }

    private void CheckLevel()
    {
        if (Vocation is Peasant && Level == 10)
        {
            return;
        }
        while (Experience >= Levelling.GuildMemberLevel[Level])
        {
            Level++;
            notificationManager.CreateNotification($"{person.name} reached Level {Level}!", Notification.Spirit.Good);

            if (Vocation is Peasant peasant)
            {
                guildhall.AdjustIncome(peasant.IncomeResource, -peasant.Income);
                peasant.Income = Mathf.CeilToInt(peasant.Income * 1.5f);
                guildhall.AdjustIncome(peasant.IncomeResource, peasant.Income);
                if (Level == 5)
                {
                    notificationManager.CreateNotification($"{person.name} can now choose a Vocation!", Notification.Spirit.Good);
                }
            }
            MaxHitpoints += 10;
            Hitpoints = MaxHitpoints;
            guildhall.CalculateArtisanProficiency();
        }
    }

    private void CheckSkillLevels()
    {
        if (Vocation is Adventurer adventurer)
        {
            while (adventurer.CombatExp >= Levelling.SkillLevel[adventurer.CombatLevel])
            {
                adventurer.CombatLevel++;
            }
            while (adventurer.EspionageExp >= Levelling.SkillLevel[adventurer.EspionageLevel])
            {
                adventurer.EspionageLevel++;
            }
            while (adventurer.DiplomacyExp >= Levelling.SkillLevel[adventurer.DiplomacyLevel])
            {
                adventurer.DiplomacyLevel++;
            }
        }
    }

    public void PromoteToAdventurer()
    {
        Peasant peasant = (Peasant)Vocation;
        guildhall.AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Adventurer();
        Experience = 0;
        Level = 1;
        Adventurer adventurer = (Adventurer)Vocation;
        adventurer.CombatLevel = 1;
        adventurer.EspionageLevel = 1;
        adventurer.DiplomacyLevel = 1;
        MaxHitpoints = 100;
        Hitpoints = MaxHitpoints;
        notificationManager.CreateNotification($"{person.name} has honed their skills and become an Adventurer!", Notification.Spirit.Good);
    }

    public void PromoteToArtisan()
    {
        Peasant peasant = (Peasant)Vocation;
        guildhall.AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Artisan();
        Experience = 0;
        Level = 1;
        MaxHitpoints = 100;
        Hitpoints = MaxHitpoints;
        notificationManager.CreateNotification($"{person.name} has honed their skills and become an Artisan!", Notification.Spirit.Good);
    }
}
