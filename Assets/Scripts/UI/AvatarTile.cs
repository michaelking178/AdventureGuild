using UnityEngine;
using UnityEngine.UI;

public class AvatarTile : MonoBehaviour
{
    private GuildMember hero;

    public void SetAvatar()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        Sprite newAvatar = GetComponentsInChildren<Image>()[1].sprite;
        hero.SetAvatar(newAvatar);
    }
}
