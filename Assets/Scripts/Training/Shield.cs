using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Vector2[] quadrants = new Vector2[4];

    [SerializeField]
    private float timeLimit = 60.0f;

    private float repositionDelay = 2.0f;
    private Vector2 swayPos;
    private int currentQuadrant;
    private int newQuadrant;

    void Start()
    {
        swayPos = new Vector2();
        StartCoroutine(Sway());
        StartCoroutine(ShieldPositioner());
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, swayPos, Time.fixedDeltaTime);
    }

    private IEnumerator ShieldPositioner()
    {
        while (repositionDelay > 0.5f)
        {
            yield return new WaitForSeconds(repositionDelay);
            ChangePosition();
        }
    }

    private IEnumerator Sway()
    {
        while (true)
        {
            swayPos.x = transform.position.x + Random.Range(-0.25f, 0.25f);
            swayPos.y = transform.position.y + Random.Range(-0.25f, 0.25f);
            yield return new WaitForSeconds(0.2f);
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
        swayPos = newPos;
        currentQuadrant = newQuadrant;
        repositionDelay *= 0.97f;
    }
}
