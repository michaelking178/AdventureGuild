using UnityEngine;

public class MenuSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void PlayMenuSound()
    {
        audioSource.Play();
    }
}
