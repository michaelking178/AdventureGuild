using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        FindObjectOfType<LevelManager>().LoadLevel(level);
    }

    public void LoadLevelDirect(string level)
    {
        FindObjectOfType<LevelManager>().LoadLevelDirect(level);
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
        if (FindObjectOfType<SoundManager>() != null)
            FindObjectOfType<SoundManager>().PlaySound("Button");
    }

    public void CallGooglePlayAchievements()
    {
        FindObjectOfType<GPGSAuthentication>().ShowAchievements();
    }

    public void CallGooglePlayLeaderboard()
    {
        FindObjectOfType<GPGSAuthentication>().ShowLeaderboard();
    }
}
