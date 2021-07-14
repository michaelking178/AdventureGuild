using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField]
    protected TextMeshProUGUI timeText;

    [SerializeField]
    protected TextMeshProUGUI scoreText;

    [Header("Results Panel")]
    [SerializeField]
    protected Image resultsImage;

    [SerializeField]
    protected TextMeshProUGUI resultsScore;

    [SerializeField]
    protected TextMeshProUGUI resultsFinalScore;

    [SerializeField]
    protected TextMeshProUGUI resultsExp;

    [SerializeField]
    protected TextMeshProUGUI resultsTotalExp;

    [SerializeField]
    protected TextMeshProUGUI adText;

    [Header("Other UI")]
    [SerializeField]
    protected Image instructionsImage;

    [SerializeField]
    protected TextMeshProUGUI countdownText;

    [SerializeField]
    protected GameObject clickBlockerPanel;

    [SerializeField]
    protected GameObject popupClickBlocker;

    protected Boost xPBoost;

    public bool GameOver;
    public bool GamePaused { get; protected set; } = false;
    public int TotalExp { get; set; } = 0;

    protected BoostManager boostManager;
    protected GuildMember guildMember;
    protected int score = 0;
    protected int exp = 0;
    protected int boostExp = 0;
    protected bool countingDown = false;
    protected float countdown;
    protected float defaultCountdown = 5.0f;
    private PopupManager popupManager;

    protected virtual void Start()
    {
        boostManager = FindObjectOfType<BoostManager>();
        popupManager = FindObjectOfType<PopupManager>();
        xPBoost = boostManager.GetComponent<TrainingXPBoost>();
    }

    protected virtual void FixedUpdate()
    {
        if (popupManager.IsPopupOpen)
            PauseGame();
        else
            ResumeGame();
    }
    public void SetGuildMember(GuildMember _guildMember)
    {
        guildMember = _guildMember;
    }

    public void OpenInstructions()
    {
        StartCoroutine(OpenInstructionsCRTN());
    }

    private IEnumerator OpenInstructionsCRTN()
    {
        yield return new WaitForSeconds(0.5f);
        if (!instructionsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open"))
            instructionsImage.GetComponent<Animator>().SetTrigger("Open");
        popupClickBlocker.SetActive(false);
    }

    public virtual void BeginTraining()
    {
        StartCoroutine(StartCountdown());
    }

    public void AddPoints(int points)
    {
        score += points;
    }
    public virtual void StopGame()
    {
        GameOver = true;
        clickBlockerPanel.SetActive(true);
        popupClickBlocker.SetActive(false);
        timeText.text = "";
        scoreText.text = "";
        resultsScore.text = $"{score}";
        if (!resultsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open"))
            resultsImage.GetComponent<Animator>().SetTrigger("Open");
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

    public virtual void ApplyResults()
    {
        popupClickBlocker.SetActive(true);
        if (!resultsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close"))
            resultsImage.GetComponent<Animator>().SetTrigger("Close");
        if (boostManager.IsTrainingExpBoosted)
            guildMember.AddExp(TotalExp + boostExp);
        else
            guildMember.AddExp(TotalExp);
        FindObjectOfType<TrainingXPBoost>().EndBoost();
        boostExp = 0;
        ResetGame();
    }

    public virtual void PauseGame()
    {
        if (!GamePaused)
        {
            GamePaused = true;
        }
    }

    public virtual void ResumeGame()
    {
        if (GamePaused)
        {
            GamePaused = false;
        }
    }

    public void DisablePopupClickBlocker()
    {
        popupClickBlocker.SetActive(false);
    }

    protected virtual IEnumerator StartCountdown()
    {
        ResetSession();
        popupClickBlocker.SetActive(true);
        if (!instructionsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close"))
            instructionsImage.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        countingDown = true;
    }

    protected void CountDown()
    {
        if (countingDown && countdown > 0f)
        {
            countdownText.gameObject.SetActive(true);
            countdown -= (Time.deltaTime * 1.25f);
            if ((int)countdown == 0)
            {
                countdownText.text = "";
                GameOver = false;
            }
            else if ((int)countdown <= 1)
            {
                countdownText.text = "GO!";
            }
            else
            {
                countdownText.text = ((int)countdown-1).ToString();
            }
        }
        else
        {
            countingDown = false;
            countdownText.gameObject.SetActive(false);
        }
    }

    protected virtual void ResetGame()
    {
        ResetSession();
        TotalExp = 0;
        guildMember = null;
    }

    protected virtual void ResetSession()
    {
        score = 0;
        exp = 0;
        countdown = defaultCountdown;
        scoreText.text = score.ToString();
        countdownText.text = "";
    }
}
