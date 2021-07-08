using System.Collections;
using TMPro;
using UnityEngine;

public class CombatTrainingManager : TrainingManager
{
    #region Data

    [Header("Scoreboard")]
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Results Panel")]
    [SerializeField]
    private TextMeshProUGUI resultsScore;

    [SerializeField]
    private TextMeshProUGUI resultsAccuracy;

    [SerializeField]
    private TextMeshProUGUI resultsFinalScore;

    [SerializeField]
    private TextMeshProUGUI resultsExp;

    [SerializeField]
    private TextMeshProUGUI resultsTotalExp;

    [Header("Game Stats")]
    public float TimeLimit = 15f;
    public float TimeRemaining = 0;

    private TrainingSword sword;
    private Shield shield;

    #endregion

    protected override void Start()
    {
        // Todo: ExpBoost debug stuff can be removed later.
        if (FindObjectOfType<PopulationManager>().DebugBoostEnabled == true) TimeLimit = 10f;

        base.Start();
        countdown = defaultCountdown;
        sword = FindObjectOfType<TrainingSword>();
        shield = FindObjectOfType<Shield>();
        GameOver = true;
        TimeRemaining = TimeLimit;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!GamePaused)
        {
            CountDown();
            if (!GameOver)
            {
                timeText.text = Helpers.FormatTimer((int)TimeRemaining);
                scoreText.text = score.ToString();
                TimeRemaining -= Time.deltaTime;
                if (TimeRemaining < 0.125f)
                {
                    TimeRemaining = 0;
                    StopGame();
                }
            }
        }
        UpdateBoostText();
    }

    public override void ApplyResults()
    {
        base.ApplyResults();
        ResetGame();
    }

    public override void ResumeGame()
    {
        if (GamePaused)
        {
            GamePaused = false;
            if (countingDown)
            {
                shield.StartTime = Time.time;
            }
        }
    }

    public void UpdateBoostText()
    {
        if (boostManager.IsTrainingExpBoosted)
        {
            resultsTotalExp.text = $"{TotalExp} (+{boostExp})";
            adText.text = "You're gaining a bonus 20% XP!";
        }
        else
        {
            resultsTotalExp.text = $"{TotalExp}";
            adText.text = $"Watch an ad to gain a bonus 20% XP (that's {boostExp} XP)!";
        }
    }

    private void StopGame()
    {
        GameOver = true;
        clickBlockerPanel.SetActive(true);
        popupClickBlocker.SetActive(false);
        StartCoroutine(ResetShield());

        timeText.text = "";
        scoreText.text = "";

        float accuracy;
        if (sword.Swings == 0 || sword.Hits == 0)
            accuracy = 0;
        else
            accuracy = (float)sword.Hits / sword.Swings;

        int accuracyPercent = Mathf.FloorToInt(accuracy * 100);

        resultsScore.text = $"{score}";
        score = Mathf.RoundToInt(score * accuracy);

        if (score == 0)
        {
            exp = 0;
        }
        else
        {
            exp = Mathf.CeilToInt(score / 10);
        }
        TotalExp += exp;
        boostExp = Mathf.CeilToInt(TotalExp * xPBoost.BoostValue);

        if (!resultsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open"))
            resultsImage.GetComponent<Animator>().SetTrigger("Open");
        resultsAccuracy.text = $"{accuracyPercent}%";
        resultsFinalScore.text = $"{score}";
        resultsExp.text = $"{exp}";
        UpdateBoostText();
    }

    private IEnumerator ResetShield()
    {
        yield return new WaitForSeconds(1.0f);
        shield.ResetPositionAndColor();
    }

    private void ResetGame()
    {
        ResetSession();
        TotalExp = 0;
        guildMember = null;

    }

    private void ResetSession()
    {
        score = 0;
        exp = 0;
        TimeRemaining = TimeLimit;
        countdown = defaultCountdown;
        shield.ResetShieldSpeed();
        timeText.text = Helpers.FormatTimer((int)TimeRemaining);
        scoreText.text = score.ToString();
        countdownText.text = "";
    }

    protected override IEnumerator StartCountdown()
    {
        ResetSession();
        return base.StartCountdown();
    }
}
