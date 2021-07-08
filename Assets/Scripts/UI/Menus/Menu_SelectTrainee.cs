using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SelectTrainee : Menu
{
    [SerializeField]
    private Scrollbar scrollbar;

    private PersonUIScrollView scrollView;

    private void Start()
    {
        scrollView = GetComponentInChildren<PersonUIScrollView>();
    }

    public override void Open()
    {
        StartCoroutine(LoadMenu());
    }

    public override void Close()
    {
        base.Close();
        StartCoroutine(ClearPersonUIs());
    }

    public void CompleteTraining()
    {
        StartCoroutine(CompleteTrainingCR());
    }

    private IEnumerator CompleteTrainingCR()
    {
        TrainingManager trainingManager = FindObjectOfType<TrainingManager>();
        trainingManager.ApplyResults();
        yield return new WaitForSeconds(0.5f);
        Open();
    }

    private IEnumerator ClearPersonUIs()
    {
        yield return new WaitForSeconds(1f);
        scrollView.ClearPersonUIs();
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Collapse();
        }
    }

    private IEnumerator LoadMenu()
    {
        FindObjectOfType<TrainingManager>().DisablePopupClickBlocker();
        yield return new WaitForSeconds(0.25f);
        base.Open();
        if (scrollView == null) scrollView = GetComponent<PersonUIScrollView>();
        scrollView.LoadAvailablePersonUIs();
        scrollView.SetPersonUIButtons(true, false, false);
        scrollbar.value = 1;
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Expand();
        }
    }
}
