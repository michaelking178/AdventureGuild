using UnityEngine;

public class TrainingPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject popupClickBlocker;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsOpenBool()
    {
        // Do nothing since it's a Training Popup
    }

    public void Close()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            animator.SetTrigger("Close");
        popupClickBlocker.SetActive(true);
    }

    public void Open()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            animator.SetTrigger("Open");
        popupClickBlocker.SetActive(false);
    }

    public void PlaySound()
    {
        Debug.Log("TrainingPopup Sound!");
    }
}
