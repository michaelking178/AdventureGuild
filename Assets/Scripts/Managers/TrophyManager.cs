using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public List<Trophy> Trophies { get; set; } = new List<Trophy>();

    [SerializeField]
    private Sprite trophySprite;

    private int renownMultiplier = 5;

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
                int renown = quest.level * renownMultiplier;
                trophy.Unlocker = quest.GuildMember.person.name;
                string unlockString = $"{trophy.Unlocker} {trophy.Description}\n\nYou earned +{renown} Renown!";
                PopupManager popupManager = FindObjectOfType<PopupManager>();
                popupManager.CallGenericPopup("Trophy Unlocked", trophy.Name, unlockString, trophySprite);
                popupManager.SetPopupButtonText("Trophies", "Close");
                popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(GoToTrophyRoom);
                FindObjectOfType<Guildhall>().AdjustRenown(renown);
            }
        }
    }

    private void GoToTrophyRoom()
    {
        FindObjectOfType<PopupManager>().GenericPopup.ConfirmBtn.onClick.RemoveListener(GoToTrophyRoom);
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
