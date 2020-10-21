using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        // ClearOldSave();
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
        FindObjectOfType<MenuManager>().CloseMenu();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    private void OnApplicationQuit()
    {
        if (GameObject.Find("Hero").GetComponent<GuildMember>().Created == true)
        {
            SaveSystem.SaveGame();
        }
    }

    private void ClearOldSave()
    {
        if (SaveSystem.SaveFileExists() && SaveSystem.GetSaveVersion() != Application.version)
        {
            SaveSystem.DeleteGame();
        }
    }
}
