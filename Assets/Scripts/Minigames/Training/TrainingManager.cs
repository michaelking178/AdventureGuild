using TMPro;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI expText;

    [SerializeField]
    private TextMeshProUGUI combatExpText;

    public float TimeLimit = 60f;
    public float TimeRemaining = 0;
    public bool GameOver = false;

    private GuildMember guildMember;
    private int score = 0;
    private int exp = 0;
    private int combatExp = 0;

    private void Start()
    {
        TimeRemaining = TimeLimit;
    }

    private void FixedUpdate()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining < 0)
        {
            TimeRemaining = 0;
            StopGame();
        }
        timeText.text = TimeRemaining.ToString();
        scoreText.text = score.ToString();
        expText.text = exp.ToString();
        combatExpText.text = combatExp.ToString();
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
    }
}
