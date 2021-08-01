using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Challenges : Menu
{
    #region Data

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private ChallengeGroup dailyChallengeGroup;

    [SerializeField]
    private ChallengeGroup weeklyChallengeGroup;

    [SerializeField]
    private GameObject challengePrefab;

    [SerializeField]
    private TextMeshProUGUI dailyTimer;

    [SerializeField]
    private TextMeshProUGUI weeklyTimer;

    private ChallengeManager challengeManager;

    #endregion

    private void Start()
    {
        challengeManager = FindObjectOfType<ChallengeManager>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            dailyTimer.text = Helpers.FormatTimer((int)challengeManager.DailyRemaining.TotalSeconds);
            weeklyTimer.text = Helpers.FormatTimer((int)challengeManager.WeeklyRemaining.TotalSeconds);
        }
    }

    public override void Open()
    {
        base.Open();
        PopulateChallenges();
        scrollbar.value = 1;
        foreach (ChallengeGroup challengeGroup in GetComponentsInChildren<ChallengeGroup>())
        {
            challengeGroup.Expand();
        }
    }

    private void PopulateChallenges()
    {
        foreach (DailyChallenge daily in FindObjectOfType<ChallengeManager>().CurrentDailies)
        {
            GameObject challenge = Instantiate(challengePrefab, dailyChallengeGroup.ContentPanel.transform);
            challenge.GetComponent<ChallengeItemRow>().SetChallenge(daily);
        }
        foreach (WeeklyChallenge weekly in FindObjectOfType<ChallengeManager>().CurrentWeeklies)
        {
            GameObject challenge = Instantiate(challengePrefab, weeklyChallengeGroup.ContentPanel.transform);
            challenge.GetComponent<ChallengeItemRow>().SetChallenge(weekly);
        }
    }
}
