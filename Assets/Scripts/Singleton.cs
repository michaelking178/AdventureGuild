using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
