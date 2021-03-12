using UnityEngine;
using TMPro;

public class Menu_Quests : Menu
{
    #region Data

    private QuestManager questManager;
    private PopulationManager populationManager;

    [SerializeField]
    private TextMeshProUGUI activeQuests;

    [SerializeField]
    private TextMeshProUGUI availableQuests;

    [SerializeField]
    private TextMeshProUGUI availableAdventurers;

    #endregion

    protected override void Start()
    {
        base.Start();
        questManager = FindObjectOfType<QuestManager>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            activeQuests.text = questManager.GetQuestsByStatus(Quest.Status.Active).Count.ToString();
            availableQuests.text = questManager.GetQuestsByStatus(Quest.Status.New).Count.ToString();
            availableAdventurers.text = populationManager.GetAvailableAdventurers().Count.ToString();
        }
    }
}
