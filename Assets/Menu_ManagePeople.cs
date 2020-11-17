using UnityEngine;
using UnityEngine.UI;

public class Menu_ManagePeople : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollbar;

    public void ResetScrollbarValue()
    {
        scrollbar.value = 0;
    }
}
