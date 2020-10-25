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
    private MenuManager menuManager;

    private void Start()
    {
        soundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        menuManager = FindObjectOfType<MenuManager>();
        LoadMenu();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            musicAudioSource.volume = musicVolume.value;
            soundAudioSource.volume = soundVolume.value;
        }
    }

    public void LoadMenu()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
    }
}
