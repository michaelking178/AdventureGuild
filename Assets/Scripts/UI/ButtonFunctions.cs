using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    private MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void OpenMenu(string menu)
    {
        menuManager.OpenMenu(menu);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }

    public void PlaySound()
    {
        FindObjectOfType<SoundManager>().PlaySound("Button");
    }
}
