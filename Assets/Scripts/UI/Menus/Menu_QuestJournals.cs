using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_QuestJournals : Menu
{
    [SerializeField]
    private QuestUIScrollView scrollView;

    public override void Open()
    {
        base.Open();
        scrollView.UpdateQuestJournalList();
    }
}
