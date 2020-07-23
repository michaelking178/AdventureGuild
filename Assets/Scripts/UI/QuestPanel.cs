using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
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
        questManager = FindObjectOfType<QuestManager>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void UpdateQuestPanel()
    {
        activeQuests.text = questManager.GetActiveQuests().Count.ToString();
        availableQuests.text = questManager.GetAvailableQuests().Count.ToString();
        availableAdventurers.text = populationManager.GetAvailableAdventurers().Count.ToString();
    }
}
