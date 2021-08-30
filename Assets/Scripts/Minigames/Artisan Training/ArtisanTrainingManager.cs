using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtisanTrainingManager : TrainingManager
{
    [Header("Artisan Training")]
    [SerializeField]
    private TextMeshProUGUI resultsTime;

    [SerializeField]
    private List<GameObject> artisanLevels = new List<GameObject>();

    private GameObject currentLevel = null;
    private int previousLevelIndex = -1;
    private float timer = 0.0f;

    protected override void Start()
    {
        base.Start();
        countdown = defaultCountdown;
        GameOver = true;
        if (FindObjectOfType<ArtisanLevel>() != null)
            currentLevel = FindObjectOfType<ArtisanLevel>().gameObject;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!GamePaused)
        {
            CountDown();
            if(!GameOver)
            {
                timer += Time.deltaTime;
                timeText.text = Helpers.FormatTimer((int)timer);
                scoreText.text = score.ToString();
            }
        }
        UpdateBoostText();
    }

    public override void BeginTraining()
    {
        base.BeginTraining();
        StartCoroutine(StartNewLevel());
    }

    public void StartLevel()
    {
        int nextLevelIndex;
        do
        {
            nextLevelIndex = Random.Range(0, artisanLevels.Count);
        } while (nextLevelIndex == previousLevelIndex);

        if (currentLevel != null)
            Destroy(currentLevel);

        int xFlip = Random.Range(0, 99);
        int yFlip = Random.Range(100, 199);

        currentLevel = Instantiate(artisanLevels[nextLevelIndex], Vector3.zero, Quaternion.identity);

        if (xFlip < 50)
            currentLevel.transform.rotation = Quaternion.Euler(currentLevel.transform.rotation.x, 180, 0);
        if (yFlip < 150)
            currentLevel.transform.rotation = Quaternion.Euler(180, currentLevel.transform.rotation.y, 0);

        previousLevelIndex = nextLevelIndex;
    }

    public override void StopGame()
    {
        base.StopGame();
        resultsTime.text = Helpers.FormatTimer((int)timer);
        int minMoves = currentLevel.GetComponent<ArtisanLevel>().MinMoves;

        if (timer != 0)
        {
            score = Mathf.RoundToInt(Mathf.Clamp((minMoves * 1200) / ((float)(score * timer) / minMoves * 0.5f), 1500.0f, 5000.0f));
            exp = Mathf.CeilToInt(score * 0.1f);
        }
        else
        {
            score = 0;
            exp = 0;
        }
        resultsFinalScore.text = score.ToString();
        resultsExp.text = exp.ToString();

        TotalExp += exp;
        boostExp = Mathf.CeilToInt(TotalExp * xPBoost.BoostValue);

        resultsTotalExp.text = TotalExp.ToString();
        UpdateBoostText();
    }

    public void AbortGame()
    {
        timer = 0;
        StopGame();
    }

    protected override void ResetSession()
    {
        base.ResetSession();
        timer = 0.0f;
        timeText.text = Helpers.FormatTimer((int)timer);
        Destroy(currentLevel);
    }

    private IEnumerator StartNewLevel()
    {
        yield return new WaitForSeconds(defaultCountdown-1);
        StartLevel();
    }
}
