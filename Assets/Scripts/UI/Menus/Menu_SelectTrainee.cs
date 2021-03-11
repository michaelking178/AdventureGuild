using System.Collections;
using UnityEngine;

public class Menu_SelectTrainee : Menu
{
    [SerializeField]
    private PersonUIScrollView personUIScrollView;

    public override void Open()
    {
        base.Open();
        personUIScrollView.ClearPersonUIs();
        personUIScrollView.LoadAdventurerUIs();
        personUIScrollView.LoadPeasantUIs();
        personUIScrollView.SetPersonUIButtons(true, false, false);
    }
}
