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
    protected GameObject popupClickBlocker;

    [SerializeField]
    protected Dimmer dimmerPanel;

    public float popupCloseDelay = 1.0f;

    protected Animator anim;
    
    private AudioSource audioSource;

    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.volume = FindObjectOfType<SoundManager>().GetComponent<AudioSource>().volume;
    }

    public virtual void Populate()
    {
        popupClickBlocker.SetActive(false);
        clickBlocker.SetActive(true);
        dimmerPanel.EnableDim();
        anim.SetTrigger("Open");
    }    

    public void Confirm()
    {
        popupClickBlocker.SetActive(true);
        dimmerPanel.DisableDim();
        anim.SetTrigger("Close");
        clickBlocker.SetActive(false);
    }

    public void Cancel()
    {
        popupClickBlocker.SetActive(true);
        dimmerPanel.DisableDim();
        ClearListeners();
        anim.SetTrigger("Close");
        clickBlocker.SetActive(false);
    }

    public void SetIsOpenBool()
    {
        FindObjectOfType<PopupManager>().SetIsOpen(false);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    private void ClearListeners()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.RemoveAllListeners();
        }
    }
}
