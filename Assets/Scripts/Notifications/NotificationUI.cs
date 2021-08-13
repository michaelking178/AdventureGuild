using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notificationText;

    [SerializeField]
    private AudioClip goodSound;

    [SerializeField]
    private AudioClip badSound;

    [SerializeField]
    private AudioClip wooshSound;

    public Notification Notification { get; set; }
    public float CloseTimer = 8.0f;

    private NotificationManager notificationManager;
    private SoundManager soundManager;
    private AudioSource audioSource;
    private AudioSource sMAudioSource;
    

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
        if (CloseTimer > 0)
        {
            CloseTimer -= Time.fixedDeltaTime;
        }
        else
        {
            Close();
        }
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
        audioSource.clip = wooshSound;
        audioSource.Play();
    }
    
    public void PlayNotificationSound()
    {
        if (Notification.NotificationSpirit == Notification.Spirit.Bad)
            audioSource.clip = badSound;
        else
            audioSource.clip = goodSound;

        audioSource.Play();
    }
}
