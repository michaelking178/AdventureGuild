using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> audioDict;

    [SerializeField]
    private AudioClip buttonPress;

    [SerializeField]
    private AudioClip menuClose;

    [SerializeField]
    private AudioClip menuOpen;

    private AudioSource audioSource;

    void Start()
    {
        audioDict = new Dictionary<string, AudioClip>();
        audioDict.Add("Button", buttonPress);
        audioDict.Add("CloseMenu", menuClose);
        audioDict.Add("OpenMenu", menuOpen);
        audioSource = GetComponent<AudioSource>();
    }

/// <summary>
/// Plays a sound called from a Dictionary: use "Button", "CloseMenu", "OpenMenu"
/// </summary>
/// <param name="sound"></param>
    public void PlaySound(string sound)
    {
        if (audioDict.TryGetValue(sound, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.Log(string.Format("Audio clip not found: {0}", clip.name));
        }
    }
}
