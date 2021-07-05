using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    [SerializeField]
    protected Image instructionsImage;

    [SerializeField]
    protected Image resultsImage;

    [SerializeField]
    protected TextMeshProUGUI countdownText;

    public bool GameOver;
    public bool GamePaused { get; protected set; } = false;

    protected GuildMember guildMember;
    protected int score = 0;
    protected int exp = 0;
    protected bool countingDown = false;
    protected float countdown;
    protected float defaultCountdown = 5.0f;
    private PopupManager popupManager;

    protected virtual void Start()
    {
        popupManager = FindObjectOfType<PopupManager>();
    }

    protected virtual void FixedUpdate()
    {
        if (popupManager.IsPopupOpen)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
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
        instructionsImage.GetComponent<Animator>().SetTrigger("Open");
    }

    public void BeginTraining()
    {
        StartCoroutine(Countdown());
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public virtual void ApplyResults()
    {
        resultsImage.GetComponent<Animator>().SetTrigger("Close");
        guildMember.AddExp(exp);
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

    protected IEnumerator Countdown()
    {
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
}
