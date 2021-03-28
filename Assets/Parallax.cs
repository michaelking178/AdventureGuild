using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private bool gyroEnabled;
    private Quaternion rot;

    private void Start()
    {
        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = new Quaternion(gyro.attitude.x, 0, 1, 0);
        }
    }
}
