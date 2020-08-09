using UnityEngine;
using UnityEngine.UI;

public class AvatarTile : MonoBehaviour
{
    private GuildMember hero;

    public void SetHeroAvatar()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        Sprite newAvatar = GetComponentsInChildren<Image>()[1].sprite;
        hero.Avatar = newAvatar;
        HeroAvatarFrame[] frames = FindObjectsOfType<HeroAvatarFrame>();
        foreach (HeroAvatarFrame frame in frames)
        {
            frame.SetFrameAvatar(hero);
        }
    }
}
