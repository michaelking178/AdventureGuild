﻿using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    #region Data

    [SerializeField]
    private Vector2[] quadrants = new Vector2[4];

    [SerializeField]
    ParticleSystem sparksPrefab;

    public float defaultRepositionDelay = 2.0f;
    public Color blue;
    public Color green;
    public Color red;
    public float StartTime { get; set; }

    private Vector2 shieldCenter = new Vector2();
    private float shieldCenterXOffset = 442/1024; // These are based on the sprite's size vs. what appears to be the center of the shield
    private float shieldCenterYOffset = 488/1024;
    private float repositionDelay;
    private float currentTime;
    private int currentQuadrant;
    private int newQuadrant;
    private bool striking = false;
    private CombatTrainingManager trainingManager;
    private TrainingSword sword;
    private SpriteRenderer spriteRenderer;
    private int pointsModifier = 2;
    private bool clickedRed = false;

    #endregion

    void Start()
    {
        sword = FindObjectOfType<TrainingSword>();
        trainingManager = FindObjectOfType<CombatTrainingManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartTime = Time.time;
        repositionDelay = defaultRepositionDelay;
        currentTime = repositionDelay;
    }

    private void FixedUpdate()
    {
        if (!trainingManager.GamePaused)
        {
            shieldCenter.x = transform.position.x + shieldCenterXOffset;
            shieldCenter.y = transform.position.y + shieldCenterYOffset;

            if (!trainingManager.GameOver)
            {
                if (spriteRenderer.color == red)
                    repositionDelay = defaultRepositionDelay * 0.5f;
                else
                    repositionDelay = defaultRepositionDelay;

                if (currentTime < repositionDelay)
                    currentTime = Time.time - StartTime;
                else
                {
                    if (spriteRenderer.color == red && !clickedRed)
                    {
                        trainingManager.AddPoints(125);
                    }
                    ChangePosition();
                    StartTime = Time.time;
                    currentTime = 0;
                    clickedRed = false;
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (!trainingManager.GamePaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!trainingManager.GameOver && hit.collider != null)
            {
                sword.Hits++;
                sword.Swing(hit.point);
                StartCoroutine(StrikeShield(hit.point));
            }
        }
    }

    public void ResetShieldSpeed()
    {
        repositionDelay = defaultRepositionDelay;
        currentTime = repositionDelay;
    }

    public void ResetPositionAndColor()
    {
        spriteRenderer.color = blue;
        transform.position = Vector3.zero;
    }

    public int PointsValue(Vector2 clickPos)
    {
        float xDifference = Mathf.Abs(shieldCenter.x - clickPos.x);
        float yDifference = Mathf.Abs(shieldCenter.y - clickPos.y);
        int points = (int)((xDifference + yDifference) * 100);
        int finalPoints = points * pointsModifier;
        return ((100 * pointsModifier) - finalPoints);
    }

    private void ChangePosition()
    {
        Vector2 newPos;
        do
        {
            newQuadrant = Random.Range(0, quadrants.Length);
            newPos = quadrants[newQuadrant];
        }
        while (newQuadrant == currentQuadrant);
        transform.position = newPos;
        currentQuadrant = newQuadrant;
        spriteRenderer.color = GetRandomColor();
        repositionDelay *= 0.98f;
        if (repositionDelay < 0.6f)
            repositionDelay = 0.6f;
    }
    
    private IEnumerator StrikeShield(Vector2 clickPos)
    {
        if (!striking)
        {
            striking = true;
            int quadrant = currentQuadrant;
            yield return new WaitForSeconds(0.15f);

            Quaternion sparkRotation = new Quaternion();
            float xRot = 0;
            if (currentQuadrant == 0) xRot = -135.0f;
            else if (currentQuadrant == 1) xRot = -45.0f;
            else if (currentQuadrant == 2) xRot = 135.0f;
            else if (currentQuadrant == 3) xRot = 45.0f;
            sparkRotation.eulerAngles = new Vector3(xRot, 90, 90);

            Instantiate(sparksPrefab, clickPos, sparkRotation);
            if (quadrant == currentQuadrant)
            {
                FindObjectOfType<TrainingSword>().ClangSound();
                if (spriteRenderer.color != red) StrikeShield(clickPos, spriteRenderer.color);
                else StrikeRedShield();
            }
            else
                FindObjectOfType<TrainingSword>().WooshSound();
            striking = false;
        }
    }

    private Color GetRandomColor()
    {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand <= 0.1f) return red;
        else if (rand <= 0.3f) return green;
        else return blue;
    }

    private void StrikeShield(Vector2 _clickPos, Color _color)
    {
        trainingManager.AddPoints(PointsValue(_clickPos));
        if (_color == blue) currentTime = repositionDelay;
    }

    private void StrikeRedShield()
    {
        clickedRed = true;
        sword.Hits--;
        trainingManager.TimeRemaining -= 3.0f;
        currentTime = repositionDelay;
    }
}
