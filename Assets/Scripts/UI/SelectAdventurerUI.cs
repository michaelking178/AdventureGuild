using UnityEngine;

public class SelectAdventurerUI : MonoBehaviour
{
    private MenuManager menuManager;
    private QuestManager questManager;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        questManager = FindObjectOfType<QuestManager>();
    }

    public void BeginQuest()
    {
        questManager.SetAdventurer(GetComponent<PersonUI>().GuildMember);
        questManager.StartQuest();
        GoToQuestJournal();
    }

    private void GoToQuestJournal()
    {
        Menu_QuestJournal questJournal = menuManager.GetMenu("Menu_QuestJournal").GetComponent<Menu_QuestJournal>();
        questJournal.SetQuest(questManager.CurrentQuest);
        questJournal.UpdateQuestJournal();
        menuManager.OpenMenu("Menu_QuestJournal");
    }
}
