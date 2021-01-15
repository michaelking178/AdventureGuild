using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationPrefab;

    [HideInInspector]
    public List<GameObject> notificationUIs;

    public List<Notification> notifications;

    private LevelManager levelManager;
    private Vector2 position;
    private float notchOffset = -200.0f;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        notifications = new List<Notification>();
        notificationUIs = new List<GameObject>();
        position = new Vector2(-3400, notchOffset);
        StartCoroutine(MoveNotificationStack());
        StartCoroutine(DisplayNotifications());
    }

    public void CreateNotification(string _notification, Notification.Spirit _spirit)
    {
        Notification notification = new Notification(_notification, _spirit);
        notifications.Add(notification);
    }

    private IEnumerator DisplayNotifications()
    {
        yield return new WaitForSeconds(4);
        while(true)
        {
            if (notifications.Count > 0)
            {
                GameObject notificationUI = Instantiate(notificationPrefab, GameObject.Find("NotificationPanel").transform);
                notificationUIs.Add(notificationUI);
                notificationUI.GetComponent<RectTransform>().anchoredPosition = position;
                notificationUI.GetComponent<NotificationUI>().Notification = notifications[0];
                switch (notifications[0].NotificationSpirit)
                {
                    case (Notification.Spirit.Good):
                        notificationUI.GetComponent<Image>().color = new Color(0, 1, 0.08719444f);
                        break;
                    case (Notification.Spirit.Bad):
                        notificationUI.GetComponent<Image>().color = new Color(1, 0.1407514f, 0);
                        break;
                    case (Notification.Spirit.Neutral):
                        notificationUI.GetComponent<Image>().color = new Color(1, 1, 0);
                        break;
                    default:
                        break;
                }
                position.y -= 490;
                notificationUI.GetComponent<Animator>().SetTrigger("Open");
                notifications.RemoveAt(0);
            }
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
                    float yPos = Mathf.Lerp(startingPos.y, (orderPos * -490) + notchOffset, 0.1f);
                    notUI.anchoredPosition = new Vector2(startingPos.x, yPos);
                }
            }
        }
    }
}
