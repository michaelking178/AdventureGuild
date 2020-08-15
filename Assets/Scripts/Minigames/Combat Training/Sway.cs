using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Vector2 sway;

    void Start()
    {
        StartCoroutine(SwayMotion());
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, sway, Time.fixedDeltaTime);
    }

    private IEnumerator SwayMotion()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            sway.x = Random.Range(-0.25f, 0.25f);
            sway.y = Random.Range(-0.25f, 0.25f);
        }
    }
}
