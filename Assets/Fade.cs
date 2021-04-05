using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float defaultFadeTime = 1.0f;

    [SerializeField]
    private bool isFadingIn = false;

    private bool isFadingOut = false;
    private float fadeTime;
    private Image image;

    void Start()
    {
        fadeTime = defaultFadeTime;
        image = GetComponent<Image>();
        if (isFadingIn) image.color = Color.black;
    }

    void FixedUpdate()
    {
        if (isFadingIn)
        {
            float alpha = image.color.a;
            alpha -= Time.fixedDeltaTime / fadeTime;
            if (alpha < 0)
            {
                alpha = 0;
                isFadingIn = false;
            }
            image.color = new Color(0, 0, 0, alpha);
        }
        else if (isFadingOut)
        {
            float alpha = image.color.a;
            alpha += Time.fixedDeltaTime / fadeTime;
            if (alpha > 1)
            {
                alpha = 1;
                isFadingOut = false;
            }
            image.color = new Color(0, 0, 0, alpha);
        }
    }

    public void FadeIn()
    {
        fadeTime = defaultFadeTime;
        isFadingIn = true;
    }

    public void FadeIn(float _fadeTime)
    {
        fadeTime = _fadeTime;
        isFadingIn = true;
    }

    public void FadeOut()
    {
        fadeTime = defaultFadeTime;
        isFadingOut = true;
    }

    public void FadeOut(float _fadeTime)
    {
        fadeTime = _fadeTime;
        isFadingOut = true;
    }
}
