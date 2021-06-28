using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericPopup : PopupMenu
{
    [SerializeField]
    private TextMeshProUGUI subtitle;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Image image;

    /// <summary>
    /// Populate the Generic Popup (if no subtitle is needed, use "")
    /// </summary>
    /// <param name="_title"></param>
    /// <param name="_subtitle"></param>
    /// <param name="_description"></param>
    /// <param name="_sprite"></param>
    public void Populate(string _title, string _subtitle, string _description, Sprite _sprite)
    {
        title.text = _title;
        subtitle.text = _subtitle;
        description.text = _description;
        image.gameObject.SetActive(true);
        image.sprite = _sprite;
        anim.SetTrigger("Open");
        GetComponent<AudioSource>().Play();
        base.Populate();
    }
}
