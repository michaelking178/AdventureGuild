using System.Collections;
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

    public override void Close()
    {
        base.Close();
        StartCoroutine(ClearQuestUIs());
    }

    private IEnumerator ClearQuestUIs()
    {
        yield return new WaitForSeconds(1);
        scrollView.ClearQuestUIs();
    }
}
