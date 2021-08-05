using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public float rotSpeed = 1.0f;
    public enum Direction { Clockwise = -1, Counterclockwise = 1 };
    public Direction RotationDirection = Direction.Clockwise;

    private RectTransform rect;
    private float zRot = 0;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        zRot += rotSpeed * Time.deltaTime * (int)RotationDirection;
        Quaternion rot = Quaternion.Euler(0, 0, zRot);
        rect.rotation = rot;
    }
}
