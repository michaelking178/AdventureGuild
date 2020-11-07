[System.Serializable]
public class SettingsData
{
    public float soundVolume;
    public float musicVolume;
    public bool debugBoostEnabled;

    public SettingsData(float _soundVol, float _musicVol, bool _debugBoostEnabled)
    {
        soundVolume = _soundVol;
        musicVolume = _musicVol;
        debugBoostEnabled = _debugBoostEnabled;
    }
}
