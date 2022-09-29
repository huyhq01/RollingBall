using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum Difficulty
{
    Easy, Medium, Hard
}
public class Setting : Singleton<Setting>
{
    [HideInInspector] public string[] difficultyArray = { Difficulty.Easy.ToString(), Difficulty.Medium.ToString(), Difficulty.Hard.ToString() };
    [HideInInspector] public string currentControlKey;

    [SerializeField] private Text difficultyText;
    [SerializeField] private GameObject bindingKey;
    [SerializeField] private AudioClip soundTest;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    private int difficultyValue;
    private AudioSource soundSource;
    public string NameOfKeyControl { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        difficultyValue = GameSetting.Instance.DifficultyValue;
        soundSource = GetComponent<AudioSource>();
        soundSource.volume = GameSetting.Instance.SoundEffectVolume;

        UpdateDifficulty(difficultyValue);

        musicSlider.value = GameSetting.Instance.MusicVolume;
        effectSlider.value = GameSetting.Instance.SoundEffectVolume;
    }

    public void GoNextDiff()
    {
        if (difficultyValue + 1 > difficultyArray.Length - 1)
        {
            return;
        }
        difficultyValue++;
        UpdateDifficulty(difficultyValue);
    }

    public void GoPrevDiff()
    {
        if (difficultyValue == 0)
        {
            return;
        }
        difficultyValue--;
        UpdateDifficulty(difficultyValue);
    }

    private void UpdateDifficulty(int value)
    {
        difficultyText.text = difficultyArray[value];
        GameSetting.Instance.DifficultyValue = value;
    }

    public void SetBindingKey(string controlName)
    {
        bindingKey.SetActive(true);
        currentControlKey = controlName;
    }

    public void BackToMenu()
    {
        GameSetting gs = GameSetting.Instance;
        SaveSetting data = new SaveSetting(
            gs.DifficultyValue,
            gs.MusicVolume,
            gs.SoundEffectVolume,
            gs.MoveLeftKey.ToString(),
            gs.MoveRightKey.ToString()
        );
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        this.gameObject.SetActive(false);
    }

    public void TestSound()
    {
        soundSource.PlayOneShot(soundTest);
    }
    public void SetVolumeMusic(float value)
    {
        GameSetting.Instance.MusicVolume = value;
        GameSetting.Instance.gameObject.GetComponent<AudioSource>().volume = GameSetting.Instance.MusicVolume;
    }
    public void SetSoundEffectVolume(float value)
    {
        GameSetting.Instance.SoundEffectVolume = value;
        soundSource.volume = GameSetting.Instance.SoundEffectVolume;
    }
    
}
