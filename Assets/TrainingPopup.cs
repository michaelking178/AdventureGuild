using UnityEngine;

public class TrainingPopup : MonoBehaviour
{
    public void SetIsOpenBool()
    {
        // Do nothing since it's a Training Popup
    }

    public void ClosePopup()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
}
