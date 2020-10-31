using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class PopupMenu : MonoBehaviour
{
    [SerializeField]
    GameObject clickBlocker;

    [SerializeField]
    private TextMeshProUGUI title, description;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Button confirmBtn, cancelBtn;

    private Animator anim;
    private GameObject caller;

    public GameObject Content;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetSingleButton(string _btnText)
    {
        cancelBtn.gameObject.SetActive(false);
        confirmBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, confirmBtn.GetComponent<RectTransform>().anchoredPosition.y);
        confirmBtn.GetComponentInChildren<TextMeshProUGUI>().text = _btnText;
    }

    public void SetDoubleButton(string _confirmText, string _cancelText)
    {
        confirmBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-107, confirmBtn.GetComponent<RectTransform>().anchoredPosition.y);
        confirmBtn.GetComponentInChildren<TextMeshProUGUI>().text = _confirmText;
        cancelBtn.gameObject.SetActive(true);
        cancelBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(107, confirmBtn.GetComponent<RectTransform>().anchoredPosition.y);
        cancelBtn.GetComponentInChildren<TextMeshProUGUI>().text = _cancelText;
    }

    public void Populate(string _title, GameObject _caller)
    {
        caller = _caller;
        title.text = _title;
        image.gameObject.SetActive(false);
        clickBlocker.SetActive(true);
        anim.SetTrigger("Open");
    }

    public void Populate(string _title, Sprite _sprite, GameObject _caller)
    {
        Populate(_title, _caller);
        image.gameObject.SetActive(true);
        image.sprite = _sprite;
    }

    public void Confirm()
    {
        anim.SetTrigger("Close");
        clickBlocker.SetActive(false);
    }

    public void Cancel()
    {
        ClearListeners();
        anim.SetTrigger("Close");
        clickBlocker.SetActive(false);
    }

    private void ClearListeners()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.RemoveAllListeners();
        }
    }
}
