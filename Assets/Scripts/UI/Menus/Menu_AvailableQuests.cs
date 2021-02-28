using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_AvailableQuests : Menu
{
    [SerializeField]
    private QuestUIScrollView scrollView;

    public override void Open()
    {
        base.Open();
        scrollView.UpdateQuestList();
    }
}
