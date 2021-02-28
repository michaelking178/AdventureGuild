using UnityEngine;
using UnityEngine.UI;

public class Menu_ManagePeople : Menu
{
    #region Data

    [SerializeField]
    private Scrollbar scrollbar;

    private PersonUIScrollView scrollView;

    #endregion

    protected override void Start()
    {
        base.Start();
        scrollView = GetComponent<PersonUIScrollView>();
    }

    public override void Open()
    {
        base.Open();
        ResetScrollbarValue();
        scrollView.GetAllPeopleUI();
    }

    public void ResetScrollbarValue()
    {
        scrollbar.value = 0;
    }
}
