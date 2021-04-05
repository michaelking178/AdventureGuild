using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericPopup : PopupMenu
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Image image;

    public void Populate(string _title, string _description, Sprite _sprite)
    {
        title.text = _title;
        description.text = _description;
        image.gameObject.SetActive(true);
        image.sprite = _sprite;
        clickBlocker.SetActive(true);
        anim.SetTrigger("Open");
    }
}
