using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Sprite positiveNotification;
    [SerializeField] private Sprite negativeNotification;
    public List<Notification> notifications;

    [HideInInspector] public List<GameObject> notificationUIs;

    private float xPosition = -1800.0f;
    private float notchOffset = -66.5f;
    private float spacing = -125.0f;
    private Vector2 position;
    private bool notificationDisplayStarted = false;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        notifications = new List<Notification>();
        notificationUIs = new List<GameObject>();
        position = new Vector2(xPosition, notchOffset);
        StartCoroutine(MoveNotificationStack());
    }

    private void FixedUpdate()
    {
        if (!notificationDisplayStarted && levelManager.CurrentLevel() != "Title")
        {
            notificationDisplayStarted = true;
            StartCoroutine(DisplayNotifications());
        }
    }

    public void CreateNotification(string _notification, Notification.Spirit _spirit)
    {
        Notification notification = new Notification(_notification, _spirit);
        notifications.Add(notification);
    }

    private IEnumerator DisplayNotifications()
    {
        yield return new WaitForSeconds(0.5f);
        Transform notificationPanel = GameObject.Find("NotificationPanel").transform;
        List<Notification> currentNotifications = new List<Notification>();
        while (true)
        {
            // Need to separate the currently iterating notifications from the master notifications list
            // to avoid alteration of the list (i.e. new notification events) during iteration.
            if (currentNotifications.Count == 0)
                currentNotifications = notifications;

            for(int i = 0; i < currentNotifications.Count; i++)
            {
                GameObject notificationUI = Instantiate(notificationPrefab, notificationPanel);
                notificationUIs.Add(notificationUI);
                notificationUI.GetComponent<RectTransform>().anchoredPosition = position;
                notificationUI.GetComponent<NotificationUI>().CloseTimer += i;
                notificationUI.GetComponent<NotificationUI>().Notification = currentNotifications[i];

                switch (currentNotifications[i].NotificationSpirit)
                {
                    case (Notification.Spirit.Good):
                        notificationUI.GetComponent<Image>().sprite = positiveNotification;
                        break;
                    case (Notification.Spirit.Bad):
                        notificationUI.GetComponent<Image>().sprite = negativeNotification;
                        break;
                    case (Notification.Spirit.Neutral):
                        notificationUI.GetComponent<Image>().sprite = positiveNotification;
                        break;
                    default:
                        break;
                }

                position.y -= spacing;
                notificationUI.GetComponent<Animator>().SetTrigger("Open");
                yield return new WaitForSeconds(0.5f);
            }
            currentNotifications.Clear();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator MoveNotificationStack()
    {
        yield return new WaitForSeconds(4);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            foreach (GameObject notification in notificationUIs)
            {
                if (notification != null)
                {
                    int orderPos = notificationUIs.IndexOf(notification);
                    RectTransform notUI = notification.GetComponent<RectTransform>();
                    Vector2 startingPos = notUI.anchoredPosition;
                    float yPos = Mathf.Lerp(startingPos.y, (orderPos * spacing) + notchOffset, 0.1f);
                    notUI.anchoredPosition = new Vector2(startingPos.x, yPos);
                }
            }
        }
    }
}
