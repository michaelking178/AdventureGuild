using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string[] compatibleVersions = { 
        "0.0.7.1", 
        "0.0.7.2", 
        "0.0.8.0", 
        "0.0.8.1",
        "0.0.8.2",
        "0.0.8.3"
    };

    private void Awake()
    {
        ClearOldSave();
    }

    private void Start()
    {
        if (SaveSystem.SaveFileExists())
        {
            SaveSystem.LoadGame();
        }
        if (CurrentLevel() == "Title")
        {
            StartCoroutine(TitleTransition());
        }
    }

    private IEnumerator TitleTransition()
    {
        yield return new WaitForSeconds(3);
        LoadLevelDirect("Main");
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
        if (SaveSystem.SaveFileExists())
        {
            bool isCompatible = false;
            if (compatibleVersions.Length != 0)
            {
                foreach (string version in compatibleVersions)
                {
                    if (SaveSystem.GetSaveVersion() == version) isCompatible = true;
                }
            }
            if (isCompatible == false) SaveSystem.DeleteGame();
        }
    }
}
