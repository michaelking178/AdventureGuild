using System;

[Serializable]
public class Adventurer : Vocation
{
    public int CombatExp { get; set; } = 0;
    public int EspionageExp { get; set; } = 0;
    public int DiplomacyExp { get; set; } = 0;
    public int MagesGuildRep { get; set; } = 0;
    public int MerchantsGuildRep { get; set; } = 0;
    public int RoyalPalaceRep { get; set; } = 0;

    public Adventurer()
    {
        title = "Adventurer";
    }
}
