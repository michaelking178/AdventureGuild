﻿using UnityEngine;

public class MenuStartPositioner : MonoBehaviour
{
    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        Vector2 position = new Vector2(5000f, 0f);
        transform.anchoredPosition = position;
    }
}
