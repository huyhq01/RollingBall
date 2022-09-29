using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum SceneName
{
    Menu,
    Game,
}

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;
    public int DifficultyValue { get; set; }
    public float MusicVolume { get; set; }
    public float SoundEffectVolume { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadSetting();
        this.gameObject.GetComponent<AudioSource>().volume = MusicVolume;
    }

    public KeyCode MoveLeftKey { get; set; }
    public KeyCode MoveRightKey { get; set; }

    void LoadSetting()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        SaveSetting save;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save = JsonUtility.FromJson<SaveSetting>(json);
        }
        else
        {
            save = new SaveSetting(0, .6f, .6f, KeyCode.LeftArrow.ToString(), KeyCode.RightArrow.ToString());
        }
        DifficultyValue = save.difficultyValue;
        SoundEffectVolume = save.soundEffectVolume;
        MusicVolume = save.musicVolume;
        MoveLeftKey =  (KeyCode) System.Enum.Parse(typeof(KeyCode), save.MoveLeftKey);
        MoveRightKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), save.MoveRightKey);
    }
}
