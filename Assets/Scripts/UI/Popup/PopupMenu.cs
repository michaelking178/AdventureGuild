using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class PopupMenu : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI title;

    public Button ConfirmBtn, CancelBtn;

    [SerializeField]
    protected GameObject clickBlocker;

    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetButtonText(string confirm)
    {
        ConfirmBtn.GetComponentInChildren<TextMeshProUGUI>().text = confirm;
    }

    public void SetButtonText(string confirm, string cancel)
    {
        SetButtonText(confirm);
        CancelBtn.GetComponentInChildren<TextMeshProUGUI>().text = cancel;
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
