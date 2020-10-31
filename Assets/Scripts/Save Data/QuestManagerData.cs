using System;

[Serializable]
public class QuestManagerData
{
    public bool CombatUnlocked;
    public bool EspionageUnlocked;
    public bool DiplomacyUnlocked;

    public QuestManagerData(QuestManager questManager)
    {
        CombatUnlocked = questManager.CombatUnlocked;
        EspionageUnlocked = questManager.EspionageUnlocked;
        DiplomacyUnlocked = questManager.DiplomacyUnlocked;
    }
}
