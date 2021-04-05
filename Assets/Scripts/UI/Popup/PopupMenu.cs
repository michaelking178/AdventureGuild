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

    [SerializeField]
    protected Dimmer dimmerPanel;

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

    public virtual void Populate()
    {
        clickBlocker.SetActive(true);
        dimmerPanel.EnableDim();
        anim.SetTrigger("Open");
    }    

    public void Confirm()
    {
        dimmerPanel.DisableDim();
        anim.SetTrigger("Close");
        clickBlocker.SetActive(false);
    }

    public void Cancel()
    {
        dimmerPanel.DisableDim();
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
