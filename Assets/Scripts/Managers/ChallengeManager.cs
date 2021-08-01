using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField]
    private int dailyTotal = 5;

    [SerializeField]
    private int weeklyTotal = 3;

    public List<DailyChallenge> CurrentDailies { get; private set; } = new List<DailyChallenge>();
    public List<WeeklyChallenge> CurrentWeeklies { get; private set; } = new List<WeeklyChallenge>();

    private DateTime challengeDay = DateTime.MinValue;
    private DateTime challengeWeek = DateTime.MinValue;

    public TimeSpan DailyRemaining { get; private set; }
    public  TimeSpan WeeklyRemaining { get; private set; }

    DailyChallenge[] dailyChallenges;
    WeeklyChallenge[] weeklyChallenges;

    private void Start()
    {
        dailyChallenges = GetComponentsInChildren<DailyChallenge>();
        weeklyChallenges = GetComponentsInChildren<WeeklyChallenge>();

        if (CurrentDailies.Count == 0)
            ResetDailyChallenges();
        if (CurrentWeeklies.Count == 0)
            ResetWeeklyChallenges();

        if (challengeDay == DateTime.MinValue)
        {
            challengeDay = DateTime.Now.Date;
        }
        if (challengeWeek == DateTime.MinValue)
        {
            challengeWeek = challengeDay;
        }
    }

    private void FixedUpdate()
    {
        DailyRemaining = challengeDay.AddDays(1) - DateTime.Now;
        WeeklyRemaining = challengeWeek.AddDays(7) - DateTime.Now;

        if (challengeDay != DateTime.Now.Date)
        {
            challengeDay = DateTime.Now.Date;
            ResetDailyChallenges();
        }
        if (DateTime.Now.Date >= challengeWeek.Date.AddDays(7))
        {
            challengeWeek = DateTime.Now.Date;
            ResetWeeklyChallenges();
        }
    }

    private void ResetDailyChallenges()
    {
        CurrentDailies.Clear();
        for (int i = 0; i < dailyTotal; i++)
        {
            AddDailyChallenge();
        }
    }

    private void ResetWeeklyChallenges()
    {
        CurrentWeeklies.Clear();
        for (int i = 0; i < weeklyTotal; i++)
        {
            AddWeeklyChallenge();
        }
    }

    private void AddDailyChallenge()
    {
        DailyChallenge challenge = dailyChallenges[UnityEngine.Random.Range(0, dailyChallenges.Length)];
        challenge.Init();
        CurrentDailies.Add(challenge);
    }

    private void AddWeeklyChallenge()
    {
        WeeklyChallenge challenge = weeklyChallenges[UnityEngine.Random.Range(0, weeklyChallenges.Length)];
        challenge.Init();
        CurrentWeeklies.Add(challenge);
    }
}
