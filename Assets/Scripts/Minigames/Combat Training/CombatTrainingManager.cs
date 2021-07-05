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
    private TextMeshProUGUI resultsCombatExp;

    [SerializeField]
    private GameObject clickBlockerPanel;

    [Header("Game Stats")]
    public float TimeLimit = 15f;
    public float TimeRemaining = 0;

    private TrainingSword sword;
    private Shield shield;
    private int combatExp = 0;

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
    }

    public override void ApplyResults()
    {
        base.ApplyResults();
        guildMember.AddExp(Quest.Skill.Combat, combatExp);
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

    private void StopGame()
    {
        GameOver = true;
        clickBlockerPanel.SetActive(true);
        StartCoroutine(ResetShield());

        timeText.text = "";
        scoreText.text = "";

        float accuracy;
        if (sword.Swings == 0 || sword.Hits == 0)
            accuracy = 0;
        else
            accuracy = (float)sword.Hits / sword.Swings;

        int accuracyPercent = Mathf.FloorToInt(accuracy * 100);

        resultsScore.text = $"Score: {score}";
        score = Mathf.RoundToInt(score * accuracy);

        if (score == 0)
        {
            exp = 0;
            combatExp = 0;
        }
        else
        {
            exp = Mathf.CeilToInt(score / 10);
            combatExp = Mathf.CeilToInt(score / 20);
        }

        resultsImage.GetComponent<Animator>().SetTrigger("Open");
        resultsAccuracy.text = $"Accuracy: {accuracyPercent}%";
        resultsFinalScore.text = $"Final Score: {score}";
        resultsExp.text = $"Experience Gained: {exp}";
        if (guildMember.Vocation is Adventurer)
        {
            resultsCombatExp.text = $"Combat Exp Gained: {combatExp}";
        }
        else
        {
            resultsCombatExp.text = $"An Adventurer would have gained {combatExp} Combat Exp!";
        }
    }

    private IEnumerator ResetShield()
    {
        yield return new WaitForSeconds(1.0f);
        shield.ResetPositionAndColor();
    }

    private void ResetGame()
    {
        score = 0;
        exp = 0;
        combatExp = 0;
        TimeRemaining = TimeLimit;
        countdown = defaultCountdown;
        guildMember = null;
        shield.ResetShieldSpeed();
        timeText.text = Helpers.FormatTimer((int)TimeRemaining);
        scoreText.text = score.ToString();
        countdownText.text = "";
    }
}
