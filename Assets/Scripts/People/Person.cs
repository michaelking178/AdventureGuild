[System.Serializable]
public class Person
{
    public string name;
    public string gender;

    public Person(int _gender, string firstName, string lastName)
    {
        if (_gender == 0)
        {
            gender = "MALE";
        }
        else
        {
            gender = "FEMALE";
        }
        name = firstName + " " + lastName;
    }
}
