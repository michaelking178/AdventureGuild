using UnityEngine;
using System.Collections.Generic;

public class PersonUIPool : MonoBehaviour
{
    public PersonUI[] PersonUIs = new PersonUI[100];

    public PersonUI GetNextAvailablePersonUI()
    {
        for (int i = 0; i < PersonUIs.Length; i++)
        {
            if (PersonUIs[i].GuildMember == null)
                return PersonUIs[i];
        }
        return null;
    }
}
