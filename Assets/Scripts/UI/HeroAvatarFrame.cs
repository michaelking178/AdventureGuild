using UnityEngine;
using UnityEngine.UI;

public class HeroAvatarFrame : MonoBehaviour
{
    private GuildMember hero;
    private Image avatarImage;

    private void Start()
    {
        avatarImage = GetComponentsInChildren<Image>()[1];
    }

    public void FixedUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>())
        {
            return;
        }
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        avatarImage.sprite = hero.GetAvatar();
    }
}
