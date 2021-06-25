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
                FindObjectOfType<PopupManager>().CallInfoPopup("Trophy Unlocked", trophy.Name, trophy.Description, trophySprite);
            }
        }
    }
}
