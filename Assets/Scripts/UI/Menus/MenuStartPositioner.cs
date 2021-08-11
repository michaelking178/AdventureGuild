using UnityEngine;

public class MenuStartPositioner : MonoBehaviour
{
    private float xStartPosition = 2475.0f;

    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        Vector2 position = new Vector2(xStartPosition, 0f);
        transform.anchoredPosition = position;
    }
}
