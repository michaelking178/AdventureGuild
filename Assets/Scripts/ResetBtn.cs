using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    public Sprite popupSprite;
    public GameObject levelManagerPrefab;

    public void CallPopup()
    {
        string description = "Are you sure you want to reset your game? All progress will be lost and this cannot be undone.";
        PopupManager popupManager = FindObjectOfType<PopupManager>();
        popupManager.CallGenericPopup("Reset Game", "", description, popupSprite);
        popupManager.GenericPopup.ConfirmBtn.onClick.AddListener(Confirm);
    }

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
        GameObject levelManagerObj = Instantiate(levelManagerPrefab);
        LevelManager levelManager = levelManagerObj.GetComponent<LevelManager>();
        levelManager.LoadLevelDirect("Title");
    }

    public void Confirm()
    {
        ResetGame();
    }
}
