using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarScrollBar : MonoBehaviour
{
    [SerializeField]
    private GameObject avatarPrefab;

    [SerializeField]
    private HeroAvatarFrame heroAvatarFrame;

    [SerializeField]
    private Scrollbar scrollbar;

    private GuildMember hero;
    private List<Sprite> avatars = new List<Sprite>();
    private PopulationManager populationManager;

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void UpdateAvatars()
    {
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        }
        ResetScrollbar();
        SetScrollbarContent();
        
        if (!HeroAvatarMatchesGender())
        {
            SetAvatarToGenderDefault();
        }

        foreach (Sprite avatar in avatars)
        {
            GameObject newCharacter = Instantiate(avatarPrefab, transform);
            Image[] images = newCharacter.GetComponentsInChildren<Image>();
            images[1].sprite = avatar;
            newCharacter.transform.GetChild(1).GetComponent<Image>().sprite = avatar;
        }
    }

    private void ResetScrollbar()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        scrollbar.value = 0;
    }

    private void SetScrollbarContent()
    {
        if (hero == null)
        {
            avatars = populationManager.maleAvatars;
            Debug.Log("No hero found!");
        }
        else if (hero.person.gender == "MALE")
        {
            avatars = populationManager.maleAvatars;
        }
        else
        {
            avatars = populationManager.femaleAvatars;
        }
    }

    private bool HeroAvatarMatchesGender()
    {
        if (hero.person.gender == "MALE")
        {
            foreach(Sprite av in populationManager.maleAvatars)
            {
                if (hero.Avatar == av)
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (Sprite av in populationManager.femaleAvatars)
            {
                if (hero.Avatar == av)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SetAvatarToGenderDefault()
    {
        if (hero.person.gender == "MALE")
        {
            hero.Avatar = populationManager.defaultMaleAvatar;
        }
        else
        {
            hero.Avatar = populationManager.defaultFemaleAvatar;
        }
        heroAvatarFrame.SetFrameAvatar(hero);
    }
}
