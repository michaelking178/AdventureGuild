using System.Collections;
using UnityEngine;

public class Menu_SelectTrainee : MonoBehaviour
{
    [SerializeField]
    private PersonUIScrollView personUIScrollView;

    private void Start()
    {
        StartCoroutine(PopulateUI());
    }

    private IEnumerator PopulateUI()
    {
        yield return new WaitForSeconds(0.1f);
        personUIScrollView.GetCombatTrainingPeopleUI();
    }
}
