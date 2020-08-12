using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationPrefab;

    [SerializeField]
    private GameObject notificationPanel;

    public List<Notification> notifications;
    public List<GameObject> notificationUIs;
    
    private Vector2 position;

    private void Start()
    {
        notifications = new List<Notification>();
        notificationUIs = new List<GameObject>();
        position = new Vector2(-3400, 0);
        StartCoroutine(MoveNotifications());
    }

    public void CreateNotification(string _notification, Notification.Type _type)
    {
        Notification notification = new Notification(_notification, _type);
        notifications.Add(notification);
        GameObject notificationUI = Instantiate(notificationPrefab, notificationPanel.transform);
        notificationUI.GetComponent<RectTransform>().anchoredPosition = position;
        notificationUI.GetComponent<NotificationUI>().Notification = notification;
        notificationUI.GetComponent<Animator>().SetTrigger("Open");
        position.y -= 490;
        notificationUIs.Add(notificationUI);
    }

    private IEnumerator MoveNotifications()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            foreach (GameObject notification in notificationUIs)
            {
                int orderPos = notificationUIs.IndexOf(notification);
                RectTransform notUI = notification.GetComponent<RectTransform>();
                Vector2 startingPos = notUI.anchoredPosition;
                float yPos = Mathf.Lerp(startingPos.y, (orderPos * -490), 0.1f);
                notUI.anchoredPosition = new Vector2(startingPos.x, yPos);
            }
        }
    }
}
