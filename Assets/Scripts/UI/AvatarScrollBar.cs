using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarScrollBar : MonoBehaviour
{
    [SerializeField]
    private GameObject avatarPrefab;

    [SerializeField]
    private List<Sprite> maleAvatars;

    [SerializeField]
    private List<Sprite> femaleAvatars;

    [SerializeField]
    private Sprite defaultMaleAvatar;

    [SerializeField]
    private Sprite defaultFemaleAvatar;

    private Person hero;
    private List<Sprite> avatars = new List<Sprite>();

    public void UpdateAvatars()
    {
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>();
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
    }

    private void SetScrollbarContent()
    {
        if (hero == null)
        {
            avatars = maleAvatars;
            Debug.Log("No hero found!");
        }
        else if (hero.GetGender() == Person.Gender.MALE)
        {
            avatars = maleAvatars;
        }
        else
        {
            avatars = femaleAvatars;
        }
    }

    private bool HeroAvatarMatchesGender()
    {
        if (hero.GetGender() == Person.Gender.MALE)
        {
            foreach(Sprite av in maleAvatars)
            {
                if (hero.GetAvatar() == av)
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (Sprite av in femaleAvatars)
            {
                if (hero.GetAvatar() == av)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SetAvatarToGenderDefault()
    {
        if (hero.GetGender() == Person.Gender.MALE)
        {
            hero.SetAvatar(defaultMaleAvatar);
        }
        else
        {
            hero.SetAvatar(defaultFemaleAvatar);
        }
    }
}
