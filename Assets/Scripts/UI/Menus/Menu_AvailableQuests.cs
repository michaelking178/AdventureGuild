using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu_AvailableQuests : Menu
{
    #region Data

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private QuestUIScrollView scrollView;

    #endregion

    public override void Open()
    {
        base.Open();
        scrollView.UpdateQuestList();
        scrollbar.value = 1;
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
