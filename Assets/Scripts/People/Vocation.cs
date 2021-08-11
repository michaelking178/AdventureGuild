[System.Serializable]
public abstract class Vocation
{
    protected string title;
    public string Title() { return title; }
    public int MaxLevel { get; protected set; }
}
