using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : Menu
{
    #region Data

    [SerializeField]
    private Slider musicVolume;

    [SerializeField]
    private Slider soundVolume;

    private AudioSource soundAudioSource;
    private AudioSource[] soundAudioSources;
    private AudioSource musicAudioSource;

    #endregion

    private void Start()
    {
        soundAudioSources = FindObjectsOfType<AudioSource>();
        soundAudioSource = FindObjectOfType<SoundManager>().GetComponent<AudioSource>();
        musicAudioSource = FindObjectOfType<MusicManager>().GetComponent<AudioSource>();
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
        }
    }

    public void LoadSettingsValues()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
    }
}
