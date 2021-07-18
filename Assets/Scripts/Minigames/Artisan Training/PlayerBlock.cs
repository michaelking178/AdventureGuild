using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField]
    private float rigidBodySpeed = 350.0f;

    [SerializeField]
    private float minSwipeLength = 30.0f;

    private enum Direction { RIGHT, LEFT, UP, DOWN };
    private Vector2 startPos;
    private bool isTouching = false;
    private bool isMoving = false;
    private bool pointAdded = false;
    private Rigidbody2D rigidBody;
    private Touch touch;
    private TrainingManager trainingManager;

    private void Start()
    {
        trainingManager = FindObjectOfType<TrainingManager>();
        touch = new Touch();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TouchControl();
    }

    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            isTouching = true;
        }

        if (isTouching && !isMoving)
        {
            if (touch.phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (SwipedRight())
                {
                    Move(Direction.RIGHT);
                }
                else if (SwipedLeft())
                {
                    Move(Direction.LEFT);
                }   
                else if (SwipedUp())
                {
                    Move(Direction.UP);
                }
                else if (SwipedDown())
                {
                    Move(Direction.DOWN);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Touch ended
                startPos = Vector2.zero;
                pointAdded = false;
            }
        }
        if (rigidBody.velocity.magnitude == 0)
            isMoving = false;
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

    private bool SwipedRight()
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (xSwipeLength >= minSwipeLength && xSwipeLength > ySwipeLength);
    }

    private bool SwipedLeft()
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (xSwipeLength <= 0 - minSwipeLength && xSwipeLength < 0 - ySwipeLength);
    }

    private bool SwipedUp()
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (ySwipeLength >= minSwipeLength && ySwipeLength > xSwipeLength);
    }

    private bool SwipedDown()
    {
        float xSwipeLength = touch.position.x - startPos.x;
        float ySwipeLength = touch.position.y - startPos.y;
        return (ySwipeLength <= 0 - minSwipeLength && ySwipeLength < 0 - xSwipeLength);
    }
}
