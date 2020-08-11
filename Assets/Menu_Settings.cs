using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : MonoBehaviour
{
    [SerializeField]
    private Slider musicVolume;

    [SerializeField]
    private Slider soundVolume;

    [SerializeField]
    private List<AudioSource> buttonAudios;

    private AudioSource soundAudioSource;
    private AudioSource musicAudioSource;

    private void Start()
    {
        soundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            if (btn.GetComponent<AudioSource>() != null)
            {
                buttonAudios.Add(btn.GetComponent<AudioSource>());
            }
        }
        LoadMenu();
    }

    private void FixedUpdate()
    {
        musicAudioSource.volume = musicVolume.value;
        soundAudioSource.volume = soundVolume.value;
        foreach (AudioSource audioSource in buttonAudios)
        {
            audioSource.volume = soundVolume.value;
        }
    }

    public void LoadMenu()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
    }

    public void ResetGame()
    {
        SaveSystem.DeleteGame();
        FindObjectOfType<LevelManager>().LoadLevel("Main");
    }
}
