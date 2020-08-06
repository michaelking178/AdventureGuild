using UnityEngine;

public class SelectAdventurerUI : MonoBehaviour
{
    private Menu_ConfirmQuest menu_ConfirmQuest;
    private MenuManager menuManager;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        menu_ConfirmQuest = menuManager.GetMenu("Menu_ConfirmQuest").GetComponent<Menu_ConfirmQuest>();
    }

    public void SetAdventurerToQuest()
    {
        FindObjectOfType<QuestManager>().SetAdventurer(GetComponent<PersonUI>().GuildMember);
    }

    public void LoadConfirmQuestMenu()
    {
        menu_ConfirmQuest.UpdateQuestMenu();
        menuManager.OpenMenu("Menu_ConfirmQuest");
    }
}
