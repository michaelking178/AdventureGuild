[System.Serializable]
public class Notification
{
    public enum Type { Quest, GuildMember }
    public enum Spirit { Good, Bad, Neutral }

    public string Message { get; set; }
    public Type NotificationType { get; set; }
    public Spirit NotificationSpirit;

    public Notification(string _message, Type _type, Spirit _spirit)
    {
        Message = _message;
        NotificationType = _type;
        NotificationSpirit = _spirit;
    }
}
