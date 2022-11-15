using UnityEngine;

public class GuildMember : MonoBehaviour
{
    public Person person;

    private Vocation vocation;
    public Vocation Vocation
    { 
        get { return vocation; }
        set {
            if (Vocation == null || Vocation.Title() == "Peasant")
                vocation = value;
            else
                Debug.Log($"{person.name} already has a vocation!");
        }
    }
    public int Id { get; set; }
    public int MaxHitpoints { get; set; } // Max HP after levelling and upgrades applied
    public int Hitpoints { get; set; } // Current HP
    public Sprite Avatar { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsIncapacitated { get; set; }
    public string Bio { get; set; }
    public bool Created { get; set; } = false;
    public string Quip { get; set; } = "";

    private int defaultMaxHP = 50;
    private NotificationManager notificationManager;

    public delegate void OnGuildMemberChallengeEvent(int value);
    public static event OnGuildMemberChallengeEvent OnAdventurerLevelUp;
    public static event OnGuildMemberChallengeEvent OnArtisanLevelUp;
    public static event OnGuildMemberChallengeEvent OnPeasantPromotion;
    public static event OnGuildMemberChallengeEvent OnExperienceGained;

    public void Init(Person _person)
    {
        person = _person;
        Id = Helpers.GenerateId();
        CalculateMaxHP();
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
        notificationManager = FindObjectOfType<NotificationManager>();
        CheckLevel();
    }

    public void AdjustHitpoints(int change)
    {
        Hitpoints += change;
        if (Hitpoints < 0)
            Hitpoints = 0;

        if (Hitpoints > MaxHitpoints)
            Hitpoints = MaxHitpoints;

        if (Hitpoints == 0)
            IsIncapacitated = true;
        else
            IsIncapacitated = false;
    }

    public void AddExp(int _exp)
    {
        if (Level < Vocation.MaxLevel)
            Experience += _exp;

        // Event for Challenges
        OnExperienceGained?.Invoke(_exp);

        CheckLevel();
    }

    private void CheckLevel()
    {
        Guildhall guildhall = FindObjectOfType<Guildhall>();

        while (Experience >= Levelling.GuildMemberLevel[Level])
        {
            if (Vocation != null && Level == Vocation.MaxLevel)
            {
                Experience = Levelling.GuildMemberLevel[Level - 1];
                return;
            }

            Level++;
            
            if (Level > 1)
                notificationManager.CreateNotification($"{person.name} reached Level {Level}!", Notification.Spirit.Good);
            if (Level == Vocation.MaxLevel)
                notificationManager.CreateNotification($"{person.name} has mastered their Vocation!", Notification.Spirit.Good);

            // Level up event for Challenges
            if (Vocation is Adventurer)
                OnAdventurerLevelUp?.Invoke(1);
            else if (Vocation is Artisan)
                OnArtisanLevelUp?.Invoke(1);

            if (Vocation is Peasant peasant)
            {
                guildhall.AdjustIncome(peasant.IncomeResource, -peasant.Income);
                CalculatePeasantIncome(peasant);
                guildhall.AdjustIncome(peasant.IncomeResource, peasant.Income);
                if (Level == 5)
                    notificationManager.CreateNotification($"{person.name} can now choose a Vocation!", Notification.Spirit.Good);
            }
            CalculateMaxHP();
            Hitpoints = MaxHitpoints;
            guildhall.CalculateArtisanProficiency();
        }
    }

    public void PromoteToAdventurer()
    {
        Peasant peasant = (Peasant)Vocation;
        FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Adventurer();
        Experience = 0;
        Level = 1;
        Adventurer adventurer = (Adventurer)Vocation;
        defaultMaxHP = 100;
        CalculateMaxHP();
        Hitpoints = MaxHitpoints;
        notificationManager.CreateNotification($"{person.name} has honed their skills and become an Adventurer!", Notification.Spirit.Good);

        // Event for Challenges
        OnPeasantPromotion?.Invoke(1);
    }

    public void PromoteToArtisan()
    {
        Peasant peasant = (Peasant)Vocation;
        FindObjectOfType<Guildhall>().AdjustIncome(peasant.IncomeResource, -peasant.Income);
        Vocation = new Artisan();
        Experience = 0;
        Level = 1;
        defaultMaxHP = 100;
        CalculateMaxHP();
        Hitpoints = MaxHitpoints;
        notificationManager.CreateNotification($"{person.name} has honed their skills and become an Artisan!", Notification.Spirit.Good);

        // Event for Challenges
        OnPeasantPromotion?.Invoke(1);
    }

    public void CalculateMaxHP()
    {
        if (Vocation is Peasant)
        {
            defaultMaxHP = 50;
            MaxHitpoints = defaultMaxHP + (10 * (Level - 1));
        }
        else if (Vocation is Artisan)
        {
            defaultMaxHP = 100;
            MaxHitpoints = defaultMaxHP + (10 * (Level - 1));
        }
        else
        {
            defaultMaxHP = 100;
            MaxHitpoints = Mathf.RoundToInt(defaultMaxHP + (10 * (Level - 1)) + FindObjectOfType<PopulationManager>().AdventurerHPUpgradeLevel * (defaultMaxHP * 0.02f));
        }
    }

    public void CalculatePeasantIncome(Peasant peasant)
    {
        peasant.Income = Mathf.RoundToInt(peasant.BaseIncome * Mathf.Pow(1.5f, Level) * (1 + (FindObjectOfType<PopulationManager>().PeasantIncomeUpgradeLevel * 0.025f)));
    }
}