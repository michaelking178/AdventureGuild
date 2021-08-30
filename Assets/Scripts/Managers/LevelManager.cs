using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string[] compatibleVersions = {
        "1.0.0.0"
    };

    private ClickBlocker clickBlocker;

    private void Awake()
    {
        ClearOldSave();
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

    private void ClearOldSave()
    {
        if (SaveSystem.SaveFileExists())
        {
            bool isCompatible = false;
            if (compatibleVersions.Length != 0)
            {
                if (SaveSystem.GetSaveVersion() == Application.version || CompatibleLegacyVersion())
                    isCompatible = true;
            }
            if (!isCompatible) SaveSystem.DeleteGame();
        }
    }

    private bool CompatibleLegacyVersion()
    {
        foreach (string version in compatibleVersions)
        {
            if (SaveSystem.GetSaveVersion() == version || SaveSystem.GetSaveVersion() == Application.version)
                return true;
        }
        return false;
    }
}
