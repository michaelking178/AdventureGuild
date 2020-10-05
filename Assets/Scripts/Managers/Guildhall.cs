using System;
using UnityEngine;

[Serializable]
public class Guildhall : MonoBehaviour
{
    public int Gold { get; set; }
    public int Iron { get; set; }
    public int Wood { get; set; }
    public int Renown { get; set; }
    public float renownThreshold = 50.0f;
    public int PopulationCap { get; set; } = 5;

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
            questManager.PopulateQuestPool(Mathf.FloorToInt(renownThreshold / 25) - Mathf.FloorToInt(renownThreshold / 100));
            renownThreshold *= 2f;
        }
    }

    public void AdjustGold(int change)
    {
        Gold += change;
        if (Gold < 0)
        {
            Gold = 0;
        }
    }

    public void AdjustIron(int change)
    {
        Iron += change;
        if (Iron < 0)
        {
            Iron = 0;
        }
    }

    public void AdjustWood(int change)
    {
        Wood += change;
        if (Wood < 0)
        {
            Wood = 0;
        }
    }

    public void AdjustRenown(int change)
    {
        Renown += change;
        if (Renown < 0)
        {
            Renown = 0;
        }
    }
}
