[System.Serializable]
public class Notification
{
    public enum Spirit { Good, Bad, Neutral }

    public string Message { get; set; }
    public Spirit NotificationSpirit;

    public Notification(string _message, Spirit _spirit)
    {
        Message = _message;
        NotificationSpirit = _spirit;
    }
}
