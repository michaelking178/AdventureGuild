using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public List<Trophy> Trophies { get; set; } = new List<Trophy>();

    [SerializeField]
    private Sprite trophySprite;

    public void AddTrophy(Quest quest)
    {
        foreach(Trophy trophy in Trophies)
        {
            if (trophy.Id == quest.id) return;
        }
        Trophy newTrophy = new Trophy(quest.id, quest.questName, quest.trophyDescription);
        Trophies.Add(newTrophy);
    }

    public void UnlockTrophy(Quest quest)
    {
        foreach(Trophy trophy in Trophies)
        {
            if (trophy.Id == quest.id)
            {
                trophy.Unlock();
                trophy.Unlocker = quest.GuildMember.person.name;
                PopupManager popupManager = FindObjectOfType<PopupManager>();
                string unlockString = $"{trophy.Unlocker} {trophy.Description}";
                popupManager.CallGenericPopup("Trophy Unlocked", trophy.Name, unlockString, trophySprite);
                popupManager.SetPopupButtonText("Trophies", "Close");
                popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(GoToTrophyRoom);
            }
        }
    }

    private void GoToTrophyRoom()
    {
        if (FindObjectOfType<LevelManager>().CurrentLevel() != "Main")
            StartCoroutine(LoadTrophyScene());
        else
            LoadTrophyRoom();
    }

    private IEnumerator LoadTrophyScene()
    {
        FindObjectOfType<LevelManager>().LoadLevel("Main");
        yield return new WaitForSeconds(1f);
        LoadTrophyRoom();
    }

    private void LoadTrophyRoom()
    {
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        Menu_TrophyRoom trophyRoom = FindObjectOfType<Menu_TrophyRoom>();
        if (menuManager.CurrentMenu != trophyRoom)
            menuManager.OpenMenu(trophyRoom);
    }
}
