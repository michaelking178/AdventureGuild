using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public float rotSpeed = 1.0f;

    private RectTransform rect;
    private float zRot = 0;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        zRot += rotSpeed * Time.deltaTime;
        Quaternion rot = Quaternion.Euler(0, 0, zRot);
        rect.rotation = rot;
    }
}
