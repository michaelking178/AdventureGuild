using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMaker : MonoBehaviour
{
    public enum Gender { FEMALE, MALE };
    private Gender gender;

    private void Start()
    {
        gender = Gender.MALE;
    }

    public void SetGender(string _gender)
    {
        if (_gender == "MALE")
        {
            gender = Gender.MALE;
        }
        else if (_gender == "FEMALE")
        {
            gender = Gender.FEMALE;
        }
        else
        {
            Debug.Log("Invalid gender selected!");
        }
    }

    public Gender GetGender()
    {
        return gender;
    }
}
