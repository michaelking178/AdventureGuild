using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
