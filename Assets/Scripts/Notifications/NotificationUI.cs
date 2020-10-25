using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notificationText;

    [SerializeField]
    private Button closeButton;

    public Notification Notification { get; set; }
    private NotificationManager notificationManager;
    private SoundManager soundManager;
    private AudioSource audioSource;
    private AudioSource sMAudioSource;
    private float closeTimer = 8.0f;

    private void Start()
    {
        notificationText.text = Notification.Message;
        notificationManager = FindObjectOfType<NotificationManager>();
        soundManager = FindObjectOfType<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        sMAudioSource = soundManager.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        audioSource.volume = sMAudioSource.volume;
        if (closeTimer > 0)
        {
            closeTimer -= Time.fixedDeltaTime;
        }
        else
        {
            Close();
        }
    }

    // Todo: Remove Notification Action if it's not needed anymore.
    public void Action()
    {
        notificationManager.Notify(this);
        Close();
    }

    public void Close()
    {
        StartCoroutine(CloseNotification());
    }

    private IEnumerator CloseNotification()
    {
        GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        notificationManager.notificationUIs.Remove(gameObject);
        notificationManager.notifications.Remove(GetComponent<NotificationUI>().Notification);
        Destroy(gameObject);
    }

    public void PlayClickSound()
    {
        soundManager.PlaySound("Button");
    }
    
    public void PlayWooshSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
