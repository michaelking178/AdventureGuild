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

    public void Init()
    {
        time = DateTime.Now.ToString();
        result = (Result)UnityEngine.Random.Range(0, 3);
        if (result != Result.Neutral)
        {
            reward = new IncidentReward(result);
        }
        switch (result)
        {
            case Result.Good:
                finalResult = goodResult;
                break;
            case Result.Bad:
                finalResult = badResult;
                break;
            case Result.Neutral:
                finalResult = neutralResult;
                break;
            default:
                break;
        }
    }
}
