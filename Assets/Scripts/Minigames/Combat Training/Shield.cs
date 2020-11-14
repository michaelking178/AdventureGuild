using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Vector2[] quadrants = new Vector2[4];

    public float defaultRepositionDelay = 2.0f;

    private Vector2 shieldCenter = new Vector2();
    private float shieldCenterXOffset = 442/1024; // These are based on the sprite's size vs. what appears to be the center of the shield
    private float shieldCenterYOffset = 488/1024;
    private float repositionDelay;
    private float startTime;
    private float currentTime;
    private int currentQuadrant;
    private int newQuadrant;
    private bool striking = false;
    private TrainingManager trainingManager;
    private TrainingSword sword;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        sword = FindObjectOfType<TrainingSword>();
        trainingManager = FindObjectOfType<TrainingManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        {
            repositionDelay = 0.6f;
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
                if (spriteRenderer.color != Color.red) StrikeShield(clickPos, spriteRenderer.color);
                else StrikeRedShield();
            }
            else
            {
                FindObjectOfType<TrainingSword>().WooshSound();
            }
            striking = false;
        }
    }

    private Color GetRandomColor()
    {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand <= 0.1f) return Color.red;
        else if (rand <= 0.3f) return Color.green;
        else return Color.white;
    }

    private void StrikeShield(Vector2 _clickPos, Color _color)
    {
        trainingManager.AddPoints(PointsValue(_clickPos));
        if (_color == Color.white) currentTime = repositionDelay;
    }

    private void StrikeRedShield()
    {
        sword.Hits--;
        trainingManager.TimeRemaining -= 3.0f;
        currentTime = repositionDelay;
    }
}
