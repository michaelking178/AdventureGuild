using TMPro;
using UnityEngine;

public class Menu_CharacterCreator_02 : Menu
{
    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    [SerializeField]
    TextMeshProUGUI nameTextBox;

    public override void Open()
    {
        avatarFrame.SetHeroFrame();
        base.Open();
    }

    public void Apply()
    {
        FindObjectOfType<HeroMaker>().SetHeroName(nameTextBox.text);
        FindObjectOfType<Menu_CharacterCreator_03>().Open();
    }
}
