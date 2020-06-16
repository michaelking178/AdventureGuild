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

    private PersonMaker character;
    private Vector2 pos;
    private List<Sprite> avatars = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<PersonMaker>();
        pos = new Vector2(50f, -60f);
        UpdateAvatars();
    }

    public void UpdateAvatars()
    {
        ResetScrollbar();
        SetAvatarGender();

        foreach (Sprite avatar in avatars)
        {
            GameObject newCharacter = Instantiate(avatarPrefab, transform);
            newCharacter.GetComponentInChildren<Image>().sprite = avatar;
            newCharacter.transform.GetChild(0).GetComponent<Image>().sprite = avatar;
            newCharacter.transform.localPosition = pos;
            pos.x += 90f;
        }
    }

    private void ResetScrollbar()
    {
        foreach (GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        pos = new Vector2(50f, -60f);
    }

    private void SetAvatarGender()
    {
        if (character.GetGender() == PersonMaker.Gender.MALE)
        {
            avatars = maleAvatars;
        }
        else
        {
            avatars = femaleAvatars;
        }
    }
}
