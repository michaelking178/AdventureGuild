using System;

[Serializable]
public class Incident
{
    public enum Result { Good, Bad, Neutral }

    public string time, description;
    public string goodResult, badResult, neutralResult;
    public string finalResult;
    public Result result;
    public IncidentReward reward;
    public string rewardMessage = "";
    public int questLevel, adventurerLevel;

    public void Init(int _questLevel, int _adventurerLevel)
    {
        questLevel = _questLevel;
        adventurerLevel = _adventurerLevel;
        time = DateTime.Now.ToString();
        CalculateResult();
        if (result != Result.Neutral)
        {
            reward = new IncidentReward(result, questLevel);
        }
    }

    private void CalculateResult()
    {
        int roll = UnityEngine.Random.Range(1, 101) - adventurerLevel + questLevel;
        int good = 40;
        int neutral = 20;

        if (roll <= good)
        {
            result = Result.Good;
            finalResult = goodResult;
        }
        else if (roll <= good + neutral)
        {
            result = Result.Neutral;
            finalResult = neutralResult;
        }
        else
        {
            result = Result.Bad;
            finalResult = badResult;
        }
    }
}
