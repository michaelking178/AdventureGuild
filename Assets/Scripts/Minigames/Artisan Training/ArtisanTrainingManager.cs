using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanTrainingManager : TrainingManager
{
    private GameObject currentLevel = null;
    private int previousLevelIndex = -1;
    private bool levelIsActive = false;

    [SerializeField]
    private List<GameObject> artisanLevels = new List<GameObject>();

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
                StartLevel();
            }
        }
    }

    public void StartLevel()
    {
        if (!levelIsActive)
        {
            levelIsActive = true;
            int nextLevelIndex;
            do
            {
                nextLevelIndex = Random.Range(0, artisanLevels.Count);
            } while (nextLevelIndex == previousLevelIndex);

            if (currentLevel != null)
                Destroy(currentLevel);

            currentLevel = Instantiate(artisanLevels[nextLevelIndex], Vector3.zero, Quaternion.identity);
            previousLevelIndex = nextLevelIndex;
        }
    }

    public void CompleteLevel()
    {
        // Call popup
        // Offer to play another level or end training
        Debug.Log("Level Complete!");
        StartCoroutine(StartNewLevel());
    }

    public override void ApplyResults()
    {
        base.ApplyResults();
    }

    private IEnumerator StartNewLevel()
    {
        yield return new WaitForSeconds(3);
        levelIsActive = false;
    }
}
