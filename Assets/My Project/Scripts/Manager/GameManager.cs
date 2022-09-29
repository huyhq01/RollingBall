using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Enum
public enum Tag
{
    Danger, Platform
}
public enum GameState
{
    Menu,
    Wait,
    Continue,
    Pause,
    Death,
    Lose,
    Restart,
}
#endregion

public class GameManager : Singleton<GameManager>
{
    GameState state;
    public static event Action<GameState> UpdateState;

    private UIGameplay _UIGameplay;
    private AudioSource soundSource;
    public int difficulty { get; private set; }
    [SerializeField] private AudioClip LoseClip, DeathClip;

    private void Start()
    {
        difficulty = GameSetting.Instance.DifficultyValue;
        _UIGameplay = UIGameplay.Instance;
        HandleState(GameState.Wait);
        UpdateDifficulty(difficulty);
        InvokeRepeating(nameof(ChangeDifficulty), 20, 20);

        soundSource = GetComponent<AudioSource>();
        soundSource.volume = GameSetting.Instance.SoundEffectVolume;
    }

    void ChangeDifficulty()
    {
        if (difficulty == 2)
        {
            CancelInvoke(nameof(ChangeDifficulty));
        }
        else
        {
            difficulty++;
            UpdateDifficulty(difficulty);
        }
    }
    void UpdateDifficulty(int _difficulty)
    {
        switch (_difficulty)
        {
            case 0:
                PlayerControl.Instance.speed = 10;
                SpawnManager.Instance.spawnRate = 1.5f;
                UpdatePlatformSpeed(2);
                break;
            case 1:
                PlayerControl.Instance.speed = 12;
                SpawnManager.Instance.spawnRate = 1f;
                UpdatePlatformSpeed(3);
                break;
            case 2:
                PlayerControl.Instance.speed = 15;
                SpawnManager.Instance.spawnRate = 0.5f;
                UpdatePlatformSpeed(5);
                break;
        }
    }

    void UpdatePlatformSpeed(int speed)
    {
        Platform[] platforms = FindObjectsOfType<Platform>();
        foreach (Platform item in platforms)
        {
            item.speed = speed;
        }
    }

    public void HandleState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Menu:
                break;
            case GameState.Wait:
                HandleWait();
                break;
            case GameState.Continue:
                break;
            case GameState.Pause:
                break;
            case GameState.Death:
                soundSource.PlayOneShot(DeathClip);
                break;
            case GameState.Lose:
                soundSource.PlayOneShot(LoseClip);
                break;
            case GameState.Restart:
                break;
        }
        UpdateState?.Invoke(newState);

    }

    private void HandleWait()
    {
        // find platform in range and put player on one of them (random)
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(Tag.Platform.ToString());
        foreach (GameObject item in platforms)
        {
            if (item.transform.position.y <= 2)
            {
                PlayerControl.Instance.gameObject.transform.position =
                    new Vector2(item.transform.position.x, item.transform.position.y + .3f);
                break;
            }
        }
    }

    public void ResumeGame()
    {
        HandleState(GameState.Continue);
    }

    public void GoToMainMenu()
    {
        HandleState(GameState.Restart);
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        HandleState(GameState.Restart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
