using System;
using System.Collections.Generic;

[Serializable]
public class ChallengeManagerData
{
    public bool ChallengesUnlocked = false;
    public List<ChallengeData> DailiesData = new List<ChallengeData>();
    public List<ChallengeData> WeekliesData = new List<ChallengeData>();
    public DateTime ChallengeDay;
    public DateTime ChallengeWeek;

    public ChallengeManagerData (ChallengeManager challengeManager)
    {
        ChallengesUnlocked = challengeManager.ChallengesUnlocked;

        foreach (Challenge challenge in challengeManager.CurrentDailies)
        {
            DailiesData.Add(new ChallengeData(challenge));
        }
        foreach (Challenge challenge in challengeManager.CurrentWeeklies)
        {
            WeekliesData.Add(new ChallengeData(challenge));
        }
        ChallengeDay = challengeManager.ChallengeDay;
        ChallengeWeek = challengeManager.ChallengeWeek;
    }
}
