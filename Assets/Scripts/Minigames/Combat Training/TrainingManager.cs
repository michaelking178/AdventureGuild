using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Instructions Panel")]
    [SerializeField]
    private Image instructionsImage;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    [Header("Results Panel")]
    [SerializeField]
    private Image resultsImage;

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
    public float TimeLimit = 30f;
    public float TimeRemaining = 0;
    public bool GameOver;

    private GuildMember guildMember;
    private TrainingSword sword;
    private int score = 0;
    private int exp = 0;
    private int combatExp = 0;
    private float countdown;
    private bool countingDown;

    private void Start()
    {
        // Todo: ExpBoost debug stuff can be removed later.
        if (FindObjectOfType<PopulationManager>().DebugBoostEnabled == true) TimeLimit = 10f;
        else TimeLimit = 30f;

        sword = FindObjectOfType<TrainingSword>();
        countdown = 3.5f;
        GameOver = true;
        TimeRemaining = TimeLimit;
    }

    private void FixedUpdate()
    {
        if (!GameOver)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining < 0)
            {
                TimeRemaining = 0;
                StopGame();
            }
        }
        if (countingDown && countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if ((int)countdown == 0)
            {
                countdownText.text = "Begin!";
            }
            else
            {
                countdownText.text = ((int)countdown).ToString();
            }
        }
        else
        {
            countingDown = false;
        }
        timeText.text = Helpers.FormatTimer((int)TimeRemaining);
        scoreText.text = score.ToString();
    }

    public void SetGuildMember(GuildMember _guildMember)
    {
        guildMember = _guildMember;
    }

    public void StartGame()
    {
        StartCoroutine(StartTraining());
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    private void StopGame()
    {
        GameOver = true;
        countdown = 3.5f;
        clickBlockerPanel.SetActive(true);

        float accuracy = (float)sword.Hits / sword.Swings;
        int accuracyPercent = Mathf.RoundToInt(accuracy * 100);

        resultsScore.text = $"Score: {score}";
        score = Mathf.RoundToInt(score * accuracy);

        exp = Mathf.CeilToInt(score / 10);
        combatExp = Mathf.CeilToInt(score / 20);
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

    private IEnumerator StartTraining()
    {
        instructionsImage.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(1);
        countingDown = true;
        yield return new WaitForSeconds(3f);
        instructionsImage.GetComponent<Animator>().SetTrigger("Close");
        GameOver = false;
    }

    public void ApplyResults()
    {
        resultsImage.GetComponent<Animator>().SetTrigger("Close");
        guildMember.AddExp(exp);
        guildMember.AddExp(Quest.Skill.Combat, combatExp);
        ResetGame();
    }

    private void ResetGame()
    {
        score = 0;
        exp = 0;
        combatExp = 0;
        countdown = 3.5f;
        TimeRemaining = TimeLimit;
        guildMember = null;
        FindObjectOfType<Shield>().ResetShieldSpeed();
        countdownText.text = "";
    }
}
