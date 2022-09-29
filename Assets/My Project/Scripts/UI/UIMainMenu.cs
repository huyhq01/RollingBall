using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingUI;

    // Start is called before the first frame update
    void Start()
    {
        SettingUI.gameObject.SetActive(false);
        SettingUI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100f, Screen.height - 60);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void Setting()
    {
        SettingUI.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SettingUI.gameObject.SetActive(false);
    }
    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
