using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField]
    private int dailyTotal = 5;

    [SerializeField]
    private int weeklyTotal = 3;

    public bool ChallengesUnlocked { get; set; } = false;
    public List<DailyChallenge> CurrentDailies { get; set; } = new List<DailyChallenge>();
    public List<WeeklyChallenge> CurrentWeeklies { get; set; } = new List<WeeklyChallenge>();

    public DateTime ChallengeDay { get; set; } = DateTime.MinValue;
    public DateTime ChallengeWeek { get; set; } = DateTime.MinValue;

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
                ResetWeeklyChallenges();

            if (ChallengeDay == DateTime.MinValue)
                ChallengeDay = DateTime.Now.Date;
            if (ChallengeWeek == DateTime.MinValue)
                SetChallengeWeekStart();
        }
    }

    private void FixedUpdate()
    {
        if (ChallengesUnlocked)
        {
            DailyRemaining = ChallengeDay.AddDays(1) - DateTime.Now;
            WeeklyRemaining = ChallengeWeek.AddDays(7) - DateTime.Now;

            if (DailyRemaining <= TimeSpan.Zero)
            {
                ResetDailyChallenges();
            }
            if (WeeklyRemaining <= TimeSpan.Zero)
            {
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
        ChallengeDay = DateTime.Now.Date;
        foreach (Challenge challenge in CurrentDailies)
        {
            challenge.ResetChallenge();
        }

        CurrentDailies.Clear();
        for (int i = 0; i < dailyTotal; i++)
        {
            AddDailyChallenge();
        }
    }

    private void ResetWeeklyChallenges()
    {
        SetChallengeWeekStart();
        foreach (Challenge challenge in CurrentWeeklies)
        {
            challenge.ResetChallenge();
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
            ChallengeWeek = DateTime.Now.Date.AddDays(-1);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            ChallengeWeek = DateTime.Now.Date.AddDays(-2);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            ChallengeWeek = DateTime.Now.Date.AddDays(-3);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            ChallengeWeek = DateTime.Now.Date.AddDays(-4);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            ChallengeWeek = DateTime.Now.Date.AddDays(-5);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            ChallengeWeek = DateTime.Now.Date.AddDays(-6);
        else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            ChallengeWeek = DateTime.Now.Date;
    }
}
