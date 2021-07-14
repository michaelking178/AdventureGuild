using System;

[Serializable]
public class Adventurer : Vocation
{
    public int QuestsCompleted { get; set; } = 0;
    public int CombatExp { get; set; } = 0;
    public int EspionageExp { get; set; } = 0;
    public int DiplomacyExp { get; set; } = 0;
    public int MagesGuildRep { get; set; } = 0;
    public int MerchantsGuildRep { get; set; } = 0;
    public int RoyalPalaceRep { get; set; } = 0;
    public int CombatLevel { get; set; } = 1;
    public int EspionageLevel { get; set; } = 1;
    public int DiplomacyLevel { get; set; } = 1;

    public Adventurer()
    {
        title = "Adventurer";
    }
}
