using UnityEngine;

public class MenuStartPositioner : MonoBehaviour
{
    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        Vector2 position = new Vector2(5000f, 0f);
        transform.anchoredPosition = position;
    }

    public void PlayOpenSound()
    {
        FindObjectOfType<SoundManager>().PlaySound("OpenMenu");
    }

    public void PlayCloseSound()
    {
        FindObjectOfType<SoundManager>().PlaySound("CloseMenu");
    }
}
