using UnityEngine;
using UnityEngine.UI;

public class HeroAvatarFrame : MonoBehaviour
{
    private Person hero;
    private Image avatarImage;

    private void Start()
    {
        avatarImage = GetComponentsInChildren<Image>()[1];
    }

    public void FixedUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>())
        {
            return;
        }
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>();
        avatarImage.sprite = hero.GetAvatar();
    }
}
