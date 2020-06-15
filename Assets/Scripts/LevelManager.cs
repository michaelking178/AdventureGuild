using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> listeners = new List<GameObject>();

    public void LoadLevel(string scene)
    {
        StartCoroutine(LevelTransition(scene));
    }

    private IEnumerator LevelTransition(string scene)
    {
        // Do all level transition animations
        foreach (GameObject listener in listeners)
        {
            listener.GetComponent<Animator>().SetTrigger("ExitLevel");
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void AddListener(GameObject listener)
    {
        listeners.Add(listener);
    }
}
