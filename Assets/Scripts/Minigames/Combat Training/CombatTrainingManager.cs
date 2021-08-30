using System.Collections;
using TMPro;
using UnityEngine;

public class CombatTrainingManager : TrainingManager
{
    #region Data

    [Header("Adventurer Training")]
    [SerializeField]
    private TextMeshProUGUI resultsAccuracy;

    [Header("Game Stats")]
    public float TimeLimit = 15f;
    public float TimeRemaining = 0;

    private TrainingSword sword;
    private Shield shield;

    #endregion

    protected override void Start()
    {
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

    public override void StopGame()
    {
        base.StopGame();
        StartCoroutine(ResetShield());

        float accuracy;
        if (sword.Swings == 0 || sword.Hits == 0)
            accuracy = 0;
        else
            accuracy = (float)sword.Hits / sword.Swings;

        int accuracyPercent = Mathf.FloorToInt(accuracy * 100);

        score = Mathf.RoundToInt(score * accuracy);

        if (score == 0)
            exp = 0;
        else
            exp = Mathf.CeilToInt(score / 10);

        TotalExp += exp;
        boostExp = Mathf.CeilToInt(TotalExp * xPBoost.BoostValue);

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
    protected override void ResetSession()
    {
        base.ResetSession();
        TimeRemaining = TimeLimit;
        timeText.text = Helpers.FormatTimer((int)TimeRemaining);
        shield.ResetShieldSpeed();
    }
}
