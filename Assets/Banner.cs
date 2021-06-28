using UnityEngine;

public class Banner : MonoBehaviour
{
    [SerializeField]
    private AudioClip openSwoosh;

    [SerializeField]
    private AudioClip closeSwoosh;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSwoosh()
    {
        audioSource.clip = openSwoosh;
        audioSource.Play();
    }

    public void PlayCloseSwoosh()
    {
        audioSource.clip = closeSwoosh;
        audioSource.Play();
    }
}
