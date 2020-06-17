using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField]
    private List<Person> people = new List<Person>();

    public void AddPerson(Person person)
    {
        people.Add(person);
    }

    public List<Person> Population()
    {
        return people;
    }

    public List<Person> Adventurers()
    {
        List<Person> adventurers = new List<Person>();
        foreach(Person person in people)
        {
            if (person.GetVocation() is Adventurer)
            {
                adventurers.Add(person);
            }
        }
        return adventurers;
    }

    public List<Person> Artisans()
    {
        List<Person> artisans = new List<Person>();
        foreach (Person person in people)
        {
            if (person.GetVocation() is Artisan)
            {
                artisans.Add(person);
            }
        }
        return artisans;
    }

    public List<Person> Peasants()
    {
        List<Person> peasants = new List<Person>();
        foreach (Person person in people)
        {
            if (person.GetVocation() is Peasant)
            {
                peasants.Add(person);
            }
        }
        return peasants;
    }
}
