using UnityEngine;

public class Menu : MonoBehaviour
{
    protected MenuManager menuManager;
    private SoundManager soundManager;
    private AudioSource menuSoundPlayer;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        menuSoundPlayer = FindObjectOfType<MenuSoundPlayer>().GetComponent<AudioSource>();
        menuManager = FindObjectOfType<MenuManager>();
        menuSoundPlayer.volume = soundManager.GetComponent<AudioSource>().volume;
    }

    // Issues all commands necessary for the menu to load successfully. This intended to clean up the OnClick() button events
    public virtual void Open() {
        menuManager.OpenMenu(this);
    }

    public virtual void Close() {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void PlayOpenSound()
    {
        menuSoundPlayer.Play();
    }
}
