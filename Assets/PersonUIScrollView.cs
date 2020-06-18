using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonUIScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject personUI;

    private PopulationManager populationManager;

    // Start is called before the first frame update
    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
        UpdatePersonList();
    }

    public void UpdatePersonList()
    {
        foreach(GameObject child in gameObject.GetChildren())
        {
            Destroy(child);
        }
        foreach (Person person in populationManager.Population())
        {
            GameObject newPersonUI = Instantiate(personUI, transform);
            newPersonUI.GetComponent<PersonUI>().SetPerson(person);
        }
    }
}
