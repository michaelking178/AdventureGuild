using UnityEngine;

public class PopupData
{
    public enum PopupType { GENERIC, INFO, QUEST };
    public PopupType popupType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Sprite Sprite { get; set; }

    public PopupData(PopupType _popupType, string _title, string _subtitle, string _desc, Sprite _sprite)
    {
        popupType = popupType;
        Title = _title;
        Subtitle = _subtitle;
        Description = _desc;
        Sprite = _sprite;
    }
}
