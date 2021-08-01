using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : Menu
{
    #region Data

    [SerializeField]
    private Slider musicVolume;

    [SerializeField]
    private Slider soundVolume;

    [SerializeField]
    private Toggle expBoostToggle;

    private AudioSource soundAudioSource;
    private AudioSource[] soundAudioSources;
    private AudioSource musicAudioSource;
    private PopulationManager populationManager;

    #endregion

    private void Start()
    {
        soundAudioSources = FindObjectsOfType<AudioSource>();
        soundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        populationManager = FindObjectOfType<PopulationManager>();
        LoadSettingsValues();
    }

    public override void Open()
    {
        base.Open();
        LoadSettingsValues();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            foreach(AudioSource audioSource in soundAudioSources)
            {
                if (audioSource != musicAudioSource)
                    audioSource.volume = soundVolume.value;
            }
            musicAudioSource.volume = musicVolume.value;

            if (expBoostToggle.isOn) populationManager.DebugBoostEnabled = true;
            else populationManager.DebugBoostEnabled = false;
        }
    }

    public void LoadSettingsValues()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
        if (populationManager.DebugBoostEnabled == true) expBoostToggle.isOn = true;
        else expBoostToggle.isOn = false;
    }
}
