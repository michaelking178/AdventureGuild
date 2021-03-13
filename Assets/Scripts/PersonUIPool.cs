using UnityEngine;

public class PersonUIPool : MonoBehaviour
{
    #region Data

    [SerializeField]
    private GameObject personUIPrefab;

    [SerializeField]
    private PersonUI[] personUIs = new PersonUI[5];

    private PopulationManager populationManager;

    #endregion

    private void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public PersonUI GetNextAvailablePersonUI()
    {
        for (int i = 0; i < personUIs.Length; i++)
        {
            if (personUIs[i].GuildMember == null)
                return personUIs[i];
        }
        return null;
    }

    private void FixedUpdate()
    {
        if (personUIs.Length < populationManager.GuildMembers.Count + 5)
        {
            PersonUI[] newPersonUIs = new PersonUI[personUIs.Length + 5];
            personUIs.CopyTo(newPersonUIs, 0);
            for(int i = 0; i < newPersonUIs.Length; i++)
            {
                if (newPersonUIs[i] == null)
                {
                    GameObject person = Instantiate(personUIPrefab, transform);
                    newPersonUIs[i] = person.GetComponent<PersonUI>();
                    newPersonUIs[i].gameObject.SetActive(false);
                }
            }
            personUIs = newPersonUIs;
        }
        else if (personUIs.Length > populationManager.GuildMembers.Count + 10)
        {
            PersonUI[] newPersonUIs = new PersonUI[personUIs.Length - 5];
            for(int i = 0; i < newPersonUIs.Length; i++)
            {
                newPersonUIs[i] = personUIs[i];
            }
            personUIs = newPersonUIs;
        }
    }
}
