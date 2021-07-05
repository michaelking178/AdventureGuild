using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class PopupMenu : MonoBehaviour
{
    #region Data

    [SerializeField]
    protected TextMeshProUGUI title;

    public Button ConfirmBtn, CancelBtn;

    [SerializeField]
    protected GameObject clickBlocker;

    [SerializeField]
    protected Dimmer dimmerPanel;

    public float popupCloseDelay = 1.0f;

    protected Animator anim;

    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Populate()
    {
        clickBlocker.SetActive(true);
        dimmerPanel.EnableDim();
        anim.SetTrigger("Open");
        GetComponent<AudioSource>().Play();
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

    public void SetIsOpenBool()
    {
        FindObjectOfType<PopupManager>().SetIsOpen(false);
    }

    private void ClearListeners()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.RemoveAllListeners();
        }
    }
}
