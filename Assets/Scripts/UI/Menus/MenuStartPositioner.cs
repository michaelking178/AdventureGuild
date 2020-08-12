using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStartPositioner : MonoBehaviour
{
    private SoundManager soundManager;

    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        Vector2 position = new Vector2(5000f, 0f);
        transform.anchoredPosition = position;
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void PlayOpenSound()
    {
        soundManager.PlaySound("OpenMenu");
    }

    public void PlayCloseSound()
    {
        soundManager.PlaySound("CloseMenu");
    }
}
