using UnityEngine;

public class PersonUIPool : MonoBehaviour
{
    [SerializeField]
    private PersonUI[] personUIs = new PersonUI[100];

    public PersonUI GetNextAvailablePersonUI()
    {
        for (int i = 0; i < personUIs.Length; i++)
        {
            if (personUIs[i].GuildMember == null)
                return personUIs[i];
        }
        return null;
    }
}
