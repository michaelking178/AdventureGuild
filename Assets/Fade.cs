using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public enum FadeType { FadeIn, FadeOut }
    public FadeType Fader;
    public float fadeTime = 1.0f;

    private Image image;
    private bool isFadingOut = false;

    void Start()
    {
        image = GetComponent<Image>();
        if (Fader == FadeType.FadeIn) image.color = Color.white;
        else image.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (Fader == FadeType.FadeIn)
        {
            float alpha = image.color.a;
            alpha -= Time.deltaTime / fadeTime;
            image.color = new Color(1, 1, 1, alpha);
            if (image.color.a <= 0) Destroy(gameObject);
        }
        else if (isFadingOut)
        {
            float alpha = image.color.a;
            alpha += Time.deltaTime / fadeTime;
            if (alpha > 1) alpha = 1;
            image.color = new Color(1, 1, 1, alpha);
        }
    }

    public void FadeOut(float _fadeTime)
    {
        fadeTime = _fadeTime;
        isFadingOut = true;
    }
}
