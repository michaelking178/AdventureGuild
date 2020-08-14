using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSword : MonoBehaviour
{
    private Vector2 swayPos;

    // Start is called before the first frame update
    void Start()
    {
        swayPos = new Vector2();
        StartCoroutine(Sway());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, swayPos, Time.fixedDeltaTime);
    }

    private IEnumerator Sway()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            swayPos.x = Random.Range(-0.25f, 0.25f);
            swayPos.y = Random.Range(-0.25f, 0.25f);
        }
    }
}
