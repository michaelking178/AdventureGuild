[System.Serializable]
public class Notification
{
    public enum Type { Quest, GuildMember }

    public string Message { get; set; }
    public Type NotificationType { get; set; }

    public Notification(string _message, Type _type)
    {
        Message = _message;
        NotificationType = _type;
    }
}
