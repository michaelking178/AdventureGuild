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
    private NotificationManager notificationManager;
    private MenuManager menuManager;

    private void Start()
    {
        notificationText.text = Notification.Message;
        notificationManager = FindObjectOfType<NotificationManager>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void Action()
    {
        if (Notification.NotificationType == Notification.Type.Quest)
        {
            GameObject.Find("Menu_QuestJournals").GetComponentInChildren<QuestUIScrollView>().UpdateQuestJournalList();
            menuManager.OpenMenu("Menu_QuestJournals");
        }
        else if (Notification.NotificationType == Notification.Type.GuildMember)
        {
            GameObject.Find("Menu_ManagePeople").GetComponentInChildren<PersonUIScrollView>().UpdatePersonList();
            menuManager.OpenMenu("Menu_ManagePeople");
        }
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
}
