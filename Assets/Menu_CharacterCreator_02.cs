using UnityEngine;

public class Menu_CharacterCreator_02 : Menu
{
    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    public override void Open()
    {
        avatarFrame.SetHeroFrame();
        base.Open();
    }

    public override void Close()
    {
        FindObjectOfType<HeroMaker>().SetHeroName();
        base.Close();
    }
}
