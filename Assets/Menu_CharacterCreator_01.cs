using UnityEngine;

public class Menu_CharacterCreator_01 : Menu
{
    [SerializeField]
    private AvatarScrollBar scrollBar;

    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    public override void Open()
    {
        scrollBar.UpdateAvatars();
        avatarFrame.SetHeroFrame();
        base.Open();
    }
}
