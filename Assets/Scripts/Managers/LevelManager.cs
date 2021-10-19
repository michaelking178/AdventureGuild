using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private ClickBlocker clickBlocker;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (SaveSystem.SaveFileExists())
        {
            SaveSystem.LoadGame();
        }
    }

    public void LoadLevel(string scene)
    {
        StartCoroutine(LoadLvl(scene));
    }

    public void LoadLevelDirect(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator LoadLvl(string scene)
    {
        Fade fader = FindObjectOfType<Fade>();
        FindObjectOfType<MenuManager>().CurrentMenu.Close();
        yield return new WaitForSeconds(1);
        if (fader != null)
        {
            fader.FadeOut();
            yield return new WaitForSeconds(fader.defaultFadeTime);
        }
        SceneManager.LoadScene(scene);
    }

    private void OnApplicationQuit()
    {
        if (GameObject.Find("Hero").GetComponent<GuildMember>().Created == true)
        {
            SaveSystem.SaveGame();
        }
    }
}
