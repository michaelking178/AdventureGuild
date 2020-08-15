using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        FindObjectOfType<LevelManager>().LoadLevel(level);
    }

    public void OpenMenu(string menu)
    {
        FindObjectOfType<MenuManager>().OpenMenu(menu);
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
