using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    public float splashTime = 1.5f;

    [SerializeField]
    private Slider slider;

    private bool isTiming = true;

    private void Start()
    {
        slider.maxValue = splashTime;
        slider.value = 0f;
    }

    private void FixedUpdate()
    {
        if (isTiming)
        {
            if (slider.value + Time.fixedDeltaTime >= slider.maxValue)
            {
                if (FindObjectOfType<AdvertisementInitializer>().IsReady())
                    StartCoroutine(FadeOut());
            }
            else if (slider.value < slider.maxValue)
                slider.value += Time.fixedDeltaTime;
        }
    }

    private IEnumerator FadeOut()
    {
        isTiming = false;
        slider.value = slider.maxValue;
        yield return new WaitForSeconds(0.5f);
        Fade fader = FindObjectOfType<Fade>();
        fader.FadeOut();
        yield return new WaitForSeconds(fader.defaultFadeTime);
        FindObjectOfType<LevelManager>().LoadLevelDirect("Main");
    }
}
