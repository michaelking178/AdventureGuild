using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dimmer : MonoBehaviour
{
    public float dimmerTint = 0.5f;
    public float dimmerTime = 0.5f;

    private Image image;
    private float alpha;
    private bool isDimming = false;
    private bool isLighting = false;
    private LevelManager levelManager;

    void Start()
    {
        image = GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        if (isDimming && image.color.a < dimmerTint)
        {
            alpha = image.color.a;
            alpha += Time.fixedDeltaTime / dimmerTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            if (image.color.a >= dimmerTint) isDimming = false;
        }
        else if (isLighting && image.color.a > 0)
        {
            alpha = image.color.a;
            alpha -= Time.fixedDeltaTime / dimmerTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            if (image.color.a <= 0)
            {
                alpha = 0;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                isLighting = false;
            }
        }
    }

    public void EnableDim()
    {
        isLighting = false;
        isDimming = true;
        PauseMinigame();
    }

    public void DisableDim()
    {
        isDimming = false;
        isLighting = true;
        ResumeMinigame();
    }

    private void PauseMinigame()
    {
        if (levelManager.CurrentLevel() == "CombatTraining" && !FindObjectOfType<CombatTrainingManager>().GameOver)
        {
            FindObjectOfType<CombatTrainingManager>().PauseGame();
        }
    }
    private void ResumeMinigame()
    {
        if (levelManager.CurrentLevel() == "CombatTraining" && FindObjectOfType<CombatTrainingManager>().GamePaused)
        {
            FindObjectOfType<CombatTrainingManager>().ResumeGame();
        }
    }
}
