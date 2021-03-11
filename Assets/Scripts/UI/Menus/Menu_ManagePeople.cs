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
        scrollView = GetComponentInChildren<PersonUIScrollView>();
    }

    public override void Open()
    {
        base.Open();
        scrollView.ClearPersonUIs();
        scrollView.LoadAllPeopleUI();
        scrollView.SetPersonUIButtons(false, true, true);
        scrollbar.value = 1;
    }
}
