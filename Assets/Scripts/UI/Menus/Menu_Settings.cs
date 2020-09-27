using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : MonoBehaviour
{
    [SerializeField]
    private Slider musicVolume;

    [SerializeField]
    private Slider soundVolume;

    private AudioSource soundAudioSource;
    private AudioSource musicAudioSource;

    private void Start()
    {
        soundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        LoadMenu();
    }

    private void FixedUpdate()
    {
        musicAudioSource.volume = musicVolume.value;
        soundAudioSource.volume = soundVolume.value;
    }

    public void LoadMenu()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
    }
}
