using UnityEngine;
using TMPro;

public class Menu_Quests : MonoBehaviour
{
    private MenuManager menuManager;
    private QuestManager questManager;
    private PopulationManager populationManager;

    [SerializeField]
    private TextMeshProUGUI activeQuests;

    [SerializeField]
    private TextMeshProUGUI availableQuests;

    [SerializeField]
    private TextMeshProUGUI availableAdventurers;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        questManager = FindObjectOfType<QuestManager>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            activeQuests.text = questManager.GetQuestsByStatus(Quest.Status.Active).Count.ToString();
            availableQuests.text = questManager.GetQuestsByStatus(Quest.Status.New).Count.ToString();
            availableAdventurers.text = populationManager.GetAvailableAdventurers().Count.ToString();
        }
    }
}
