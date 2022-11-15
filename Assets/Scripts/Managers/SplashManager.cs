using System.Collections;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    public float defaultsplashTime = 2.5f;

    private float splashTime;
    private bool isTiming = true;

    private void Start()
    {
        splashTime = defaultsplashTime;
    }

    private void FixedUpdate()
    {
        if (isTiming)
        {
            if (splashTime - Time.fixedDeltaTime <= 0.0f)
            {
                StartCoroutine(FadeOut());
            }
            else
                splashTime -= Time.fixedDeltaTime;
        }
    }

    private IEnumerator FadeOut()
    {
        isTiming = false;
        yield return new WaitForSeconds(0.5f);
        Fade fader = FindObjectOfType<Fade>();
        fader.FadeOut();
        yield return new WaitForSeconds(fader.defaultFadeTime);
        FindObjectOfType<LevelManager>().LoadLevelDirect("Main");
    }
}
