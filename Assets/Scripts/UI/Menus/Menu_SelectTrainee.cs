using UnityEngine;

public class Menu_SelectTrainee : Menu
{
    [SerializeField]
    private PersonUIScrollView scrollView;

    public override void Open()
    {
        Debug.Log("Opening Menu_SelectTrainee...");
        base.Open();
        Debug.Log("Requesting ScrollView LoadAvailablePersonUIs...");
        scrollView.LoadAvailablePersonUIs();
        scrollView.SetPersonUIButtons(true, false, false);
    }
}
