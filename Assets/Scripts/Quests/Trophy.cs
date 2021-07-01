using System;

[Serializable]
public class Trophy
{
    public int Id = 0;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Unlocker { get; set; } = string.Empty;
    public DateTime UnlockDate { get; private set; } = DateTime.MinValue;
    public bool IsUnlocked { get; private set; } = false;

    public Trophy(int _id, string _name, string _description)
    {
        Id = _id;
        Name = _name;
        Description = _description;
    }

    public void Unlock()
    {
        if (!IsUnlocked)
        {
            UnlockDate = DateTime.Now;
            IsUnlocked = true;
        }
    }
}
