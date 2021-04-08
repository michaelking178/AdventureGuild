using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_CharacterCreator_03 : Menu
{
    [SerializeField]
    private HeroCreatedPanel heroPanel;

    [SerializeField]
    private HeroAvatarFrame avatarFrame;

    public override void Open()
    {
        heroPanel.UpdateHeroCreatedPanel();
        avatarFrame.SetHeroFrame();
        base.Open();
    }
}
