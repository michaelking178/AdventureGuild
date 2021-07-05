using UnityEngine;

public class InfoPopupData : PopupData
{
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Sprite Sprite { get; set; } = null;

    public InfoPopupData(string _title, string _subtitle, string _desc, Sprite _sprite)
    {
        Title = _title;
        Subtitle = _subtitle;
        Description = _desc;
        Sprite = _sprite;
        ConfirmText = "Okay";
    }

    public InfoPopupData(string _title, string _subtitle, string _desc, Sprite _sprite, string _confirmText)
    {
        Title = _title;
        Subtitle = _subtitle;
        Description = _desc;
        Sprite = _sprite;
        ConfirmText = _confirmText;
    }
}
