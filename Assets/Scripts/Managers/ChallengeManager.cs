using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField]
    private int dailyTotal = 5;

    [SerializeField]
    private int weeklyTotal = 3;

    public bool ChallengesUnlocked { get; private set; } = false;
    public List<DailyChallenge> CurrentDailies { get; private set; } = new List<DailyChallenge>();
    public List<WeeklyChallenge> CurrentWeeklies { get; private set; } = new List<WeeklyChallenge>();

    private DateTime challengeDay = DateTime.MinValue;
    private DateTime challengeWeek = DateTime.MinValue;

    public TimeSpan DailyRemaining { get; private set; }
    public  TimeSpan WeeklyRemaining { get; private set; }

    private DailyChallenge[] dailyChallenges;
    private WeeklyChallenge[] weeklyChallenges;

    private void Start()
    {
        dailyChallenges = GetComponentsInChildren<DailyChallenge>();
        weeklyChallenges = GetComponentsInChildren<WeeklyChallenge>();

        if (ChallengesUnlocked)
        {
            if (CurrentDailies.Count == 0)
                ResetDailyChallenges();

            if (CurrentWeeklies.Count == 0)
            {
                SetChallengeWeekStart();
                ResetWeeklyChallenges();
            }

            if (challengeDay == DateTime.MinValue)
                challengeDay = DateTime.Now.Date;
            if (challengeWeek == DateTime.MinValue)
                challengeWeek = challengeDay;
        }
    }

    private void FixedUpdate()
    {
        if (ChallengesUnlocked)
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
    }

    public void UnlockChallenges()
    {
        ChallengesUnlocked = true;
    }

    private void ResetDailyChallenges()
    {
        foreach (Challenge challenge in CurrentDailies)
        {
            challenge.EndChallenge();
        }

        CurrentDailies.Clear();
        for (int i = 0; i < dailyTotal; i++)
        {
            AddDailyChallenge();
        }
    }

    private void ResetWeeklyChallenges()
    {
        foreach(Challenge challenge in CurrentWeeklies)
        {
            challenge.EndChallenge();
        }
        CurrentWeeklies.Clear();

        for (int i = 0; i < weeklyTotal; i++)
        {
            AddWeeklyChallenge();
        }
    }

    private void AddDailyChallenge()
    {
        bool isUnique = true;
        DailyChallenge challenge = dailyChallenges[UnityEngine.Random.Range(0, dailyChallenges.Length)];
        foreach(Challenge dailyChallenge in CurrentDailies)
        {
            if (challenge == dailyChallenge)
            {
                isUnique = false;
            }
        }

        if (!isUnique)
            AddDailyChallenge();
        else
        {
            challenge.Init();
            CurrentDailies.Add(challenge);
        }
    }

    private void AddWeeklyChallenge()
    {
        bool isUnique = true;
        WeeklyChallenge challenge = weeklyChallenges[UnityEngine.Random.Range(0, weeklyChallenges.Length)];
        foreach (Challenge weeklyChallenge in CurrentWeeklies)
        {
            if (challenge == weeklyChallenge)
            {
                isUnique = false;
            }
        }

        if (!isUnique)
            AddWeeklyChallenge();
        else
        {
            challenge.Init();
            CurrentWeeklies.Add(challenge);
        }
    }

    private void SetChallengeWeekStart()
    {
        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            challengeWeek = DateTime.Now.Date.AddDays(-1);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            challengeWeek = DateTime.Now.Date.AddDays(-2);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            challengeWeek = DateTime.Now.Date.AddDays(-3);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            challengeWeek = DateTime.Now.Date.AddDays(-4);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            challengeWeek = DateTime.Now.Date.AddDays(-5);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            challengeWeek = DateTime.Now.Date.AddDays(-6);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            challengeWeek = DateTime.Now.Date;
    }
}
