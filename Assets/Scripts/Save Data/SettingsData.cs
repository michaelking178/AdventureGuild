[System.Serializable]
public class SettingsData
{
    public float soundVolume;
    public float musicVolume;

    public SettingsData(float _soundVol, float _musicVol)
    {
        soundVolume = _soundVol;
        musicVolume = _musicVol;
    }
}
