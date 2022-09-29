
[System.Serializable]
public class SaveSetting
{
    public int difficultyValue;
    public float musicVolume;
    public float soundEffectVolume;
    public string MoveLeftKey;
    public string MoveRightKey;

    public SaveSetting(int _difficultyValue, float _musicVolume, float _soundEffectVolume, string _MoveLeftKey, string _MoveRightKey)
    {
        difficultyValue = _difficultyValue;
        musicVolume = _musicVolume;
        soundEffectVolume = _soundEffectVolume;
        MoveLeftKey = _MoveLeftKey;
        MoveRightKey = _MoveRightKey;
    }
    public SaveSetting()
    {

    }

    override
    public string ToString()
    {
        return "{" +
            difficultyValue + "; " +
            musicVolume + "; " +
            soundEffectVolume + "; " +
            MoveLeftKey + "; " +
            MoveRightKey +
             "}";
    }
}
