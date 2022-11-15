using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField]
    private float rigidBodySpeed;

    [SerializeField]
    private float minSwipeLength;

    private enum Direction { RIGHT, LEFT, UP, DOWN };
    private Vector2 startPos;
    private bool isMoving = false;
    private bool pointAdded = false;
    private Rigidbody2D rigidBody;
    private TrainingManager trainingManager;

    private void Start()
    {
        trainingManager = FindObjectOfType<TrainingManager>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TouchControl();
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.normalized.magnitude == 0)
        {
            isMoving = false;
        }
    }

    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!isMoving)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startPos = Input.GetTouch(0).position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (SwipedRight(touch))
                    {
                        Move(Direction.RIGHT);
                    }
                    else if (SwipedLeft(touch))
                    {
                        Move(Direction.LEFT);
                    }
                    else if (SwipedUp(touch))
                    {
                        Move(Direction.UP);
                    }
                    else if (SwipedDown(touch))
                    {
                        Move(Direction.DOWN);
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                pointAdded = false;
            }
        }
    }

    private void Move(Direction moveDir)
    {
        if (!isMoving)
        {
            isMoving = true;
            AddPoint();
            if (moveDir == Direction.RIGHT)
            {
                rigidBody.AddForce(Vector2.right * rigidBodySpeed, ForceMode2D.Impulse);
            }
            else if (moveDir == Direction.LEFT)
            {
                rigidBody.AddForce(Vector2.left * rigidBodySpeed, ForceMode2D.Impulse);
            }
            else if (moveDir == Direction.UP)
            {
                rigidBody.AddForce(Vector2.up * rigidBodySpeed, ForceMode2D.Impulse);
            }
            else if (moveDir == Direction.DOWN) 
            {
                rigidBody.AddForce(Vector2.down * rigidBodySpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void AddPoint()
    {
        if(!pointAdded)
        {
            pointAdded = true;
            trainingManager.AddPoints(1);
        }
    }

    private bool SwipedRight(Touch touch)
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (xSwipeLength >= minSwipeLength && xSwipeLength > ySwipeLength);
    }

    private bool SwipedLeft(Touch touch)
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (xSwipeLength <= 0 - minSwipeLength && xSwipeLength < 0 - ySwipeLength);
    }

    private bool SwipedUp(Touch touch)
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (ySwipeLength >= minSwipeLength && ySwipeLength > xSwipeLength);
    }

    private bool SwipedDown(Touch touch)
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (ySwipeLength <= 0 - minSwipeLength && ySwipeLength < 0 - xSwipeLength);
    }
}
