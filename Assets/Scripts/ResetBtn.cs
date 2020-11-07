using UnityEngine;
using UnityEngine.UI;

public class ResetBtn : MonoBehaviour
{
    public Sprite popupSprite;

    public void CallPopup()
    {
        string description = "Are you sure you want to reset your game? All progress will be lost and this cannot be undone.";
        PopupManager popupManager = FindObjectOfType<PopupManager>();
        popupManager.CreateDefaultContent(description);
        popupManager.SetDoubleButton("Reset", "Cancel");
        popupManager.Popup.GetComponentInChildren<Button>().onClick.AddListener(Confirm);

        popupManager.Populate("Reset Game", popupSprite, gameObject);
        popupManager.CallPopup();
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
        LevelManager levelManager = new LevelManager();
        levelManager.LoadLevelDirect("Main");
    }

    public void Confirm()
    {
        ResetGame();
    }
}
