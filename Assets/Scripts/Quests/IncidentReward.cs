using UnityEngine;

[System.Serializable]
public class IncidentReward
{
    public int Gold = 0;
    public int Iron = 0;
    public int Wood = 0;
    public int Hitpoints = 0;
    public int Experience = 0;
    private int questLevel;
    private int goldMax;

    public IncidentReward(Incident.Result _result, int _questLevel)
    {
        questLevel = _questLevel;
        goldMax = Mathf.CeilToInt(50 * (1 + (questLevel / 10)));
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

        int ironMax = Mathf.CeilToInt(25 * (1 + (questLevel / 10)));
        int woodMax = Mathf.CeilToInt(40 * (1 + (questLevel / 10)));
        int expMax = Mathf.CeilToInt(100 * (1 + (questLevel / 10)));

        int chance = (Random.Range(0, 101));

        if (chance > 50)
        {
            Gold = Random.Range(Gold, goldMax);
        }
        if (chance > 75)
        {
            Iron = Random.Range(Iron, ironMax);
        }
        if (chance > 60)
        {
            Wood = Random.Range(Wood, woodMax);
        }
        if (chance > 50)
        {
            Experience = Random.Range(Experience, expMax);
        }
    }

    private void GenerateBadReward()
    {
        Gold = -10;
        Hitpoints = -5;

        int hitpointsMax = Mathf.CeilToInt(20 * (1 + (questLevel / 10)));
        int chance = Random.Range(0, 101);

        if (chance > 50)
        {
            Gold = Random.Range(-goldMax, Gold);
        }
        if (chance > 25)
        {
            Hitpoints = Random.Range(-hitpointsMax, Hitpoints);
        }
    }
}
