using UnityEngine;
using UnityEngine.UI;

public class AvatarTile : MonoBehaviour
{
    private Person hero;

    public void SetAvatar()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Person>();
        Sprite newAvatar = GetComponentsInChildren<Image>()[1].sprite;
        hero.SetAvatar(newAvatar);
    }
}
