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
            foreach(GameObject child in gameObject.GetChildren())
            {
                child.SetActive(true);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
