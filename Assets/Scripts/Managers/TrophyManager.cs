using System.Collections.Generic;
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
                trophy.Description = $"{quest.GuildMember.person.name} {trophy.Description}";
                PopupManager popupManager = FindObjectOfType<PopupManager>();
                popupManager.CallGenericPopup("Trophy Unlocked", trophy.Name, trophy.Description, trophySprite);
                popupManager.SetPopupButtonText("Trophies", "Close");
                popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(GoToTrophyRoom);
            }
        }
    }

    private void GoToTrophyRoom()
    {
        FindObjectOfType<MenuManager>().OpenMenu(FindObjectOfType<Menu_TrophyRoom>());
    }
}
