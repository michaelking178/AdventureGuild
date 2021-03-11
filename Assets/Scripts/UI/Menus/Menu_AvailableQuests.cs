using System.Collections;
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
