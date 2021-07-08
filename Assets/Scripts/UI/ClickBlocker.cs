using System.Collections;
using UnityEngine;

public class ClickBlocker : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelayedClickBlockerDisable());
    }

    private IEnumerator DelayedClickBlockerDisable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
