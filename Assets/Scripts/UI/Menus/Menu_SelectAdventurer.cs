using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SelectAdventurer : Menu
{
    private PersonUIScrollView scrollView;

    protected override void Start()
    {
        base.Start();
        scrollView = GetComponentInChildren<PersonUIScrollView>();
    }

    public override void Open()
    {
        base.Open();
        scrollView.ClearPersonUIs();
        scrollView.LoadAvailableAdventurerUIs();
        scrollView.SetPersonUIButtons(true, false, false);
    }
}
