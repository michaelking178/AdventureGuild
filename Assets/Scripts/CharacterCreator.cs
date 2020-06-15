using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public enum Gender { FEMALE, MALE };

    private Gender gender;

    private void Start()
    {
        gender = Gender.MALE;
    }

    public void SetGenderToMale()
    {
        gender = Gender.MALE;
    }

    public void SetGenderToFemale()
    {
        gender = Gender.FEMALE;
    }

    public Gender GetGender()
    {
        return gender;
    }
}
