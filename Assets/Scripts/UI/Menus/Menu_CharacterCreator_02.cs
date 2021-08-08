using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_CharacterCreator_02 : Menu
{
    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    [SerializeField]
    TextMeshProUGUI nameTextBox;

    [SerializeField]
    private Button continueButton;

    public override void Open()
    {
        avatarFrame.SetHeroFrame();
        base.Open();
    }

    private void FixedUpdate()
    {
        if (nameTextBox.text.Length <= 1)
            continueButton.interactable = false;
        else
            continueButton.interactable = true;
    }

    public void Apply()
    {
        FindObjectOfType<HeroMaker>().SetHeroName(nameTextBox.text);
        FindObjectOfType<Menu_CharacterCreator_03>().Open();
    }
}
