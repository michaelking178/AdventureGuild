using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanTrainingManager : TrainingManager
{
    private GameObject currentLevel = null;
    private int previousLevelIndex = -1;

    [SerializeField]
    private List<GameObject> artisanLevels = new List<GameObject>();

    private void Start()
    {
        if (FindObjectOfType<ArtisanLevel>() != null)
            currentLevel = FindObjectOfType<ArtisanLevel>().gameObject;
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

        currentLevel = Instantiate(artisanLevels[nextLevelIndex], Vector3.zero, Quaternion.identity);
        previousLevelIndex = nextLevelIndex;
    }

    public void CompleteLevel()
    {
        // Call popup
        // Offer to play another level or end training
        Debug.Log("Level Complete!");
        StartCoroutine(StartNewLevel());
    }

    private IEnumerator StartNewLevel()
    {
        yield return new WaitForSeconds(3);
        StartLevel();
    }
}
