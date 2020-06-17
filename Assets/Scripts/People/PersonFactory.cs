using UnityEngine;

public class PersonFactory : MonoBehaviour
{
    // Do non-hero people need avatars?
    // I need lists of names, avatars, and a gender roller
    public void CreatePerson()
    {
        GameObject newPerson = new GameObject();
        newPerson.transform.SetParent(FindObjectOfType<PopulationManager>().transform);
        Person person = newPerson.AddComponent<Person>();
        person.SetGender(RandomGender());
        if (person.GetGender() == Person.Gender.MALE)
        {
            person.SetName("Jimmy McGill");
        }
        else
        {
            person.SetName("Suzy O'Malley");
        }
        newPerson.name = person.GetName();
        PrintPerson(person);
    }

    private Person.Gender RandomGender()
    {
        return (Person.Gender)Random.Range(0, 2);
    }

    private void PrintPerson(Person person)
    {
        Debug.Log(string.Format("{0} - {1}", person.GetName(), person.GetVocation()));
    }
}
