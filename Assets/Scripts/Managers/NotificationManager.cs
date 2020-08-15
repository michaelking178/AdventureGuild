using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationPrefab;

    public List<Notification> notifications;
    public List<GameObject> notificationUIs;

    private Vector2 position;
    private float notchOffset = -200.0f;

    private void Start()
    {
        notifications = new List<Notification>();
        notificationUIs = new List<GameObject>();
        position = new Vector2(-3400, notchOffset);
        StartCoroutine(MoveNotifications());
    }

    public void CreateNotification(string _notification, Notification.Type _type, Notification.Spirit _spirit)
    {
        Notification notification = new Notification(_notification, _type, _spirit);
        notifications.Add(notification);
        GameObject notificationUI = Instantiate(notificationPrefab, GameObject.Find("NotificationPanel").transform);
        notificationUI.GetComponent<RectTransform>().anchoredPosition = position;
        notificationUI.GetComponent<NotificationUI>().Notification = notification;
        switch (notification.NotificationSpirit)
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
                float yPos = Mathf.Lerp(startingPos.y, (orderPos * -490) + notchOffset, 0.1f);
                notUI.anchoredPosition = new Vector2(startingPos.x, yPos);
            }
        }
    }

    public void Notify(Notification notification)
    {
        if (FindObjectOfType<LevelManager>().CurrentLevel() != "Main")
        {
            FindObjectOfType<LevelManager>().LoadLevel("Main");
        }
        if (notification.NotificationType == Notification.Type.Quest)
        {
            GameObject.Find("Menu_QuestJournals").GetComponentInChildren<QuestUIScrollView>().UpdateQuestJournalList();
            FindObjectOfType<MenuManager>().OpenMenu("Menu_QuestJournals");
        }
        else if (notification.NotificationType == Notification.Type.GuildMember)
        {
            GameObject.Find("Menu_ManagePeople").GetComponentInChildren<PersonUIScrollView>().GetAllGuildMembers();
            FindObjectOfType<MenuManager>().OpenMenu("Menu_ManagePeople");
        }
    }
}
