using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStartPositioner : MonoBehaviour
{
    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        Vector2 position = new Vector2(1000f, 0f);
        transform.anchoredPosition = position;
    }
}
