using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    [Header("JSON Resources")]
    [SerializeField]
    private TextAsset maleNamesJson;

    [SerializeField]
    private TextAsset femaleNamesJson;

    [SerializeField]
    private TextAsset lastNamesJson;


    [Header("Name Lists")]
    [SerializeField]
    private Names maleNames;

    [SerializeField]
    private Names femaleNames;

    [SerializeField]
    private Names lastNames;

    private void Awake()
    {
        maleNames = JsonUtility.FromJson<Names>(maleNamesJson.text);
        femaleNames = JsonUtility.FromJson<Names>(femaleNamesJson.text);
        lastNames = JsonUtility.FromJson<Names>(lastNamesJson.text);
    }

    public string FirstName(string gender)
    {
        string firstName = "";
        gender.ToLower();
        try
        {
            if (gender == "male")
            {
                firstName = maleNames.prefixes[Random.Range(0, maleNames.prefixes.Length)] + maleNames.suffixes[Random.Range(0, maleNames.suffixes.Length)];
            }
            else if (gender == "female")
            {
                firstName = femaleNames.prefixes[Random.Range(0, femaleNames.prefixes.Length)] + femaleNames.suffixes[Random.Range(0, femaleNames.suffixes.Length)];
            }
        }
        catch
        {
            Debug.LogWarning($"Invalid gender selected: {gender}");
        }
        return firstName;
    }

    public string LastName()
    {
        string lastName = "";
        try
        {
            lastName = lastNames.prefixes[Random.Range(0, lastNames.prefixes.Length)] + lastNames.suffixes[Random.Range(0, lastNames.suffixes.Length)];
        }
        catch
        {
            Debug.LogWarning("LastName() failed to generate a last name!");
        }
        return lastName;
    }

    public string FullName(string gender)
    {
        return $"{FirstName(gender)} {LastName()}";
    }
}
