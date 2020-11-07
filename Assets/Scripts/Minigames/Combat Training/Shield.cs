using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Vector2[] quadrants = new Vector2[4];

    private Vector2 shieldCenter = new Vector2();
    private float shieldCenterXOffset = 442/1024; // These are based on the sprite's size vs. what appears to be the center of the shield
    private float shieldCenterYOffset = 488/1024;

    public float defaultRepositionDelay = 2.0f;

    private float repositionDelay;
    private float startTime;
    private float currentTime;
    private int currentQuadrant;
    private int newQuadrant;
    private bool striking = false;
    private TrainingManager trainingManager;
    private TrainingSword sword;

    void Start()
    {
        sword = FindObjectOfType<TrainingSword>();
        trainingManager = FindObjectOfType<TrainingManager>();
        startTime = Time.time;
        repositionDelay = defaultRepositionDelay;
        currentTime = repositionDelay;
    }

    private void FixedUpdate()
    {
        shieldCenter.x = transform.position.x + shieldCenterXOffset;
        shieldCenter.y = transform.position.y + shieldCenterYOffset;

        if (!trainingManager.GameOver)
        {
            if (currentTime < repositionDelay)
            {
                currentTime = Time.time - startTime;
            }
            else
            {
                ChangePosition();
                startTime = Time.time;
                currentTime = 0;
            }
        }
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
        repositionDelay *= 0.98f;
        if (repositionDelay < 0.6f)
        {
            repositionDelay = 0.6f;
        }
    }

    public void OnMouseDown()
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
    
    private IEnumerator StrikeShield(Vector2 clickPos)
    {
        if (!striking)
        {
            striking = true;
            int quadrant = currentQuadrant;
            yield return new WaitForSeconds(0.15f);
            if (quadrant == currentQuadrant)
            {
                FindObjectOfType<TrainingSword>().ClangSound();
                trainingManager.AddPoints(PointsValue(clickPos));
                currentTime = repositionDelay;
            }
            else
            {
                FindObjectOfType<TrainingSword>().WooshSound();
            }
            striking = false;
        }
    }

    public void ResetShieldSpeed()
    {
        repositionDelay = defaultRepositionDelay;
        currentTime = repositionDelay;
    }

    public int PointsValue(Vector2 clickPos)
    {
        float xDifference = Mathf.Abs(shieldCenter.x - clickPos.x);
        float yDifference = Mathf.Abs(shieldCenter.y - clickPos.y);
        int modifier = (int)((xDifference + yDifference) * 100);
        return (100 - modifier);
    }
}
