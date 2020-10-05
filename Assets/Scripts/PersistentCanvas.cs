using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    public static GameObject persistentCanvas;

    private void Awake()
    {
        if (persistentCanvas == null)
        {
            persistentCanvas = gameObject;
            DontDestroyOnLoad(persistentCanvas);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
