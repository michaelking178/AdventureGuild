using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float movementSpeed;
    public float xMin = -8.0f;
    public float xMax = 8.0f;
    public float xStartingPos = 8.0f;

    private new Transform transform;
    private bool isMovingRight = true;

    private void Start()
    {
        transform = GetComponent<Transform>();
        Vector3 startingPos = transform.position;
        startingPos.x = xStartingPos;
        transform.position = startingPos;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < xMin || transform.position.x > xMax)
        {
            isMovingRight = !isMovingRight;
        }

        if (isMovingRight)
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
}
