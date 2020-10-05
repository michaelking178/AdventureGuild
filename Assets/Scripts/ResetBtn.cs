using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    public void ResetGame()
    {
        SaveSystem.DeleteGame();
        if (GameObject.Find("Persistents") != null)
        {
            Destroy(GameObject.Find("Persistents"));
        }
        if (GameObject.Find("PersistentCanvas") != null)
        {
            Destroy(GameObject.Find("PersistentCanvas"));
        }
        LevelManager levelManager = new LevelManager();
        levelManager.LoadLevelDirect("Main");
    }
}
