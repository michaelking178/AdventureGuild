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

    [SerializeField]
    private Button actionButton;

    public Notification Notification { get; set; }

    private void Start()
    {
        notificationText.text = Notification.Message;
    }

    private void Action()
    {
        // Perform notification action
    }

    public void Close()
    {
        StartCoroutine(CloseNotification());
    }

    private IEnumerator CloseNotification()
    {
        GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<NotificationManager>().RemoveNotification(gameObject);
        Destroy(gameObject);
    }
}
