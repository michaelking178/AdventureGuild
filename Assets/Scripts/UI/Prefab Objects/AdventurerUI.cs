using UnityEngine;

public class AdventurerUI : MonoBehaviour
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
        Menu_QuestJournal questJournal = FindObjectOfType<Menu_QuestJournal>();
        questJournal.SetQuest(questManager.CurrentQuest);
        questJournal.UpdateQuestJournal();
        menuManager.OpenMenu(questJournal);
    }
}
