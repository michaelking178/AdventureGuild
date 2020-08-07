using System;
using UnityEngine;

[Serializable]
public class Guildhall : MonoBehaviour
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Weapons { get; set; }
    public int Renown { get; set; }

    private float renownThreshold = 50.0f;
    private PopulationManager populationManager;
    private QuestManager questManager;

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void FixedUpdate()
    {
        if (Renown >= renownThreshold)
        {
            populationManager.CreateGuildMember();
            questManager.PopulateQuestPool(Mathf.FloorToInt(renownThreshold / 20));
            renownThreshold *= 1.5f;
        }
    }

    public void AdjustGold(int change)
    {
        Gold += change;
    }

    public void AdjustIron(int change)
    {
        Iron += change;
    }

    public void AdjustWood(int change)
    {
        Wood += change;
    }

    public void AdjustWeapons(int change)
    {
        Weapons += change;
    }

    public void AdjustRenown(int change)
    {
        Renown += change;
    }
}
