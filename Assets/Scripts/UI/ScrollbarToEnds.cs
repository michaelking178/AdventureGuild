using UnityEngine;
using UnityEngine.UI;

public class ScrollbarToEnds : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 1.0f;

    [SerializeField]
    private Scrollbar scrollbar;

    private bool scrollToTop = false;
    private bool scrollToBottom = false;

    private void FixedUpdate()
    {
        if (scrollbar != null && scrollbar.gameObject.activeInHierarchy)
        {
            if (scrollToBottom)
            {
                if (scrollbar.value > 0)
                    scrollbar.value -= (Time.fixedDeltaTime * scrollSpeed);
                else scrollToBottom = false;
            }
            else if (scrollToTop)
            {
                if (scrollbar.value < 1)
                    scrollbar.value += (Time.fixedDeltaTime * scrollSpeed);
                else scrollToTop = false;
            }
        }
        else
        {
            scrollToTop = false;
            scrollToBottom = false;
        }
    }

    public void ScrollToTop()
    {
        scrollToTop = true;
    }

    public void ScrollToBottom()
    {
        scrollToBottom = true;
    }
}
