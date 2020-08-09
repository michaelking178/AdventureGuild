using UnityEngine;
using UnityEngine.UI;

public class HeroAvatarFrame : MonoBehaviour
{
    private Image avatarImage;

    private void Awake()
    {
        avatarImage = GetComponentsInChildren<Image>()[1];
    }

    public void SetFrameAvatar(GuildMember guildMember)
    {
        avatarImage.sprite = guildMember.Avatar;
    }
}
