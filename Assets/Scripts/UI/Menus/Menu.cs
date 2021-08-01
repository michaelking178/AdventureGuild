using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    protected AudioClip openSound;

    [SerializeField]
    protected AudioClip closeSound;

    protected MenuManager menuManager;
    protected AudioSource audioSource;
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        menuManager = FindObjectOfType<MenuManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = soundManager.GetComponent<AudioSource>().volume;
    }

    // Issues all commands necessary for the menu to load successfully. This intended to clean up the OnClick() button events
    public virtual void Open() {
        menuManager.OpenMenu(this);
    }

    public virtual void Close() {
        GetComponent<Animator>().SetTrigger("Close"); ;
    }

    public void PlayOpenSound()
    {
        audioSource.clip = openSound;
        audioSource.Play();
    }
}
