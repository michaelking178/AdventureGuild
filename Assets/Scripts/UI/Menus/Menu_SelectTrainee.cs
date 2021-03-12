using UnityEngine;

public class Menu_SelectTrainee : Menu
{
    [SerializeField]
    private PersonUIScrollView scrollView;

    public override void Open()
    {
        base.Open();
        scrollView.LoadAvailablePersonUIs();
        scrollView.SetPersonUIButtons(true, false, false);
    }
}
