using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField]
    private float rigidBodySpeed = 350.0f;

    [SerializeField]
    private float swipeLength = 30.0f;

    private enum Direction { RIGHT, LEFT, UP, DOWN };
    private Vector2 startPos;
    private bool isTouching = false;
    private bool isMoving = false;
    private Rigidbody2D rigidBody;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        touch = new Touch();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
                if (touch.position.x >= startPos.x + swipeLength)
                {
                    Move(Direction.RIGHT);
                }
                else if (touch.position.x <= startPos.x - swipeLength)
                {
                    Move(Direction.LEFT);
                }   
                else if (touch.position.y >= startPos.y + swipeLength)
                {
                    Move(Direction.UP);
                }
                    
                else if (touch.position.y <= startPos.y - swipeLength)
                {
                    Move(Direction.DOWN);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Touch ended
                startPos = Vector2.zero;
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
            if (moveDir == Direction.RIGHT)
            {
                rigidBody.AddForce(Vector2.right * rigidBodySpeed);
            }
            else if (moveDir == Direction.LEFT)
            {
                rigidBody.AddForce(Vector2.left * rigidBodySpeed);
            }
            else if (moveDir == Direction.UP)
            {
                rigidBody.AddForce(Vector2.up * rigidBodySpeed);
            }
            else if (moveDir == Direction.DOWN) 
            {
                rigidBody.AddForce(Vector2.down * rigidBodySpeed);
            }
        }
    }
}
