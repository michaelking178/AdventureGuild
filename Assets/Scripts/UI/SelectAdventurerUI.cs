using UnityEngine;

public class SelectAdventurerUI : MonoBehaviour
{
    private Menu_ConfirmQuest menu_ConfirmQuest;
    private MenuManager menuManager;

    void Start()
    {
        menu_ConfirmQuest = GameObject.Find("Menu_ConfirmQuest").GetComponent<Menu_ConfirmQuest>();
        menuManager = FindObjectOfType<MenuManager>();
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
