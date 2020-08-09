using UnityEngine;

[System.Serializable]
public class IncidentReward
{
    public int Gold = 0;
    public int Iron = 0;
    public int Wood = 0;
    public int Hitpoints = 0;
    public int Experience = 0;

    public IncidentReward(Incident.Result _result)
    {
        if (_result == Incident.Result.Good)
        {
            GenerateGoodReward();
        }
        else if (_result == Incident.Result.Bad)
        {
            GenerateBadReward();
        }
    }

    private void GenerateGoodReward()
    {
        Gold = 10;
        Iron = 0;
        Wood = 0;
        Experience = 20;

        int chance = Random.Range(0, 101);

        if (chance > 50)
        {
            Gold = Random.Range(Gold, 50);
        }
        if (chance > 75)
        {
            Iron = Random.Range(Iron, 25);
        }
        if (chance > 60)
        {
            Wood = Random.Range(Wood, 50);
        }
        if (chance > 50)
        {
            Experience = Random.Range(Experience, 100);
        }
    }

    private void GenerateBadReward()
    {
        Gold = -10;
        Hitpoints = -5;

        int chance = Random.Range(0, 101);

        if (chance > 50)
        {
            Gold = Random.Range(-50, Gold);
        }
        if (chance > 25)
        {
            Hitpoints = Random.Range(-20, Hitpoints);
        }
    }
}
