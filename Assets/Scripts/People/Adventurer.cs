using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Person))]
public class Adventurer : MonoBehaviour
{
    private bool isHero;
    private Person person;

    private void Start()
    {
        person = GetComponent<Person>();
    }

    public Adventurer(bool _isHero)
    {
        isHero = _isHero;
    }
}
