using UnityEngine;

public class GenericPopupData : PopupData
{
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Sprite Sprite { get; set; } = null;

    public string CancelText { get; set; } = "Cancel";

    public GenericPopupData(string _title, string _subtitle, string _desc, Sprite _sprite)
    {
        Title = _title;
        Subtitle = _subtitle;
        Description = _desc;
        Sprite = _sprite;
    }

    public GenericPopupData(string _title, string _subtitle, string _desc, Sprite _sprite, string _confirmText, string _cancelText)
    {
        Title = _title;
        Subtitle = _subtitle;
        Description = _desc;
        Sprite = _sprite;
        ConfirmText = _confirmText;
        CancelText = _cancelText;
    }
}
