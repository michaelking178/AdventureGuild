using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : MonoBehaviour
{
    [SerializeField]
    private Slider musicVolume;

    [SerializeField]
    private Slider soundVolume;

    [SerializeField]
    private Toggle expBoostToggle;

    private AudioSource soundAudioSource;
    private AudioSource musicAudioSource;
    private MenuManager menuManager;
    private PopulationManager populationManager;

    private void Start()
    {
        soundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        menuManager = FindObjectOfType<MenuManager>();
        populationManager = FindObjectOfType<PopulationManager>();
        LoadMenu();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject)
        {
            musicAudioSource.volume = musicVolume.value;
            soundAudioSource.volume = soundVolume.value;

            if (expBoostToggle.isOn) populationManager.DebugBoostEnabled = true;
            else populationManager.DebugBoostEnabled = false;
        }
    }

    public void LoadMenu()
    {
        musicVolume.value = musicAudioSource.volume;
        soundVolume.value = soundAudioSource.volume;
        if (populationManager.DebugBoostEnabled == true) expBoostToggle.isOn = true;
        else expBoostToggle.isOn = false;
    }
}
