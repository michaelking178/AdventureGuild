using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        if (SaveSystem.SaveFileExists())
        {
            SaveSystem.LoadGame();
        }
    }

    public void LoadLevel(string scene)
    {
        //FindObjectOfType<NotificationManager>().ClearNotificationUIs();
        StartCoroutine(LoadLvl(scene));
    }

    public void LoadLevelDirect(string scene)
    {
        //FindObjectOfType<NotificationManager>().ClearNotificationUIs();
        SceneManager.LoadScene(scene);
    }

    public string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator LoadLvl(string scene)
    {
        FindObjectOfType<MenuManager>().CloseMenu();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
