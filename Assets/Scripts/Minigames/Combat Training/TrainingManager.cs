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

    [SerializeField]
    private TextMeshProUGUI expText;

    [SerializeField]
    private TextMeshProUGUI combatExpText;

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
    private TextMeshProUGUI resultsExp;

    [SerializeField]
    private TextMeshProUGUI resultsCombatExp;

    [Header("Game Stats")]
    public float TimeLimit = 60f;
    public float TimeRemaining = 0;
    public bool GameOver;

    private GuildMember guildMember;
    private int score = 0;
    private int exp = 0;
    private int combatExp = 0;
    private float countdown;
    private bool countingDown;

    private void Start()
    {
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
        expText.text = exp.ToString();
        combatExpText.text = combatExp.ToString();
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
        exp += Mathf.FloorToInt(points / 10);
        combatExp = Mathf.FloorToInt(exp / 2);
    }

    private void StopGame()
    {
        GameOver = true;
        countdown = 3.5f;
        resultsImage.GetComponent<Animator>().SetTrigger("Open");
        resultsScore.text = string.Format("Score: {0}", score.ToString());
        resultsExp.text = string.Format("Experience Gained: {0}", exp.ToString());
        resultsCombatExp.text = string.Format("Combat Exp Gained: {0}", combatExp.ToString());
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
    }
}
