using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Array = System.Array;
public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] Text scoreText, LifeText, countDownText, totalScoreText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject LoseMenu;
    [SerializeField] GameObject InputField;
    [SerializeField] Image TransparentBackground;
    private int time { get; set; }
    private int playerHighscore { get; set; }
    private string playerName { get; set; }
    private PlayerData[] highscores = new PlayerData[10];
    private void Awake()
    {
        GameManager.UpdateState += UIGameplayOnStateChange;
        InputField.SetActive(false);
        TransparentBackground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

    }
    void UIGameplayOnStateChange(GameState state)
    {
        if (state == GameState.Wait)
        {
            InvokeCountDown();
        }

        TransparentBackground.gameObject.SetActive(state == GameState.Pause || state == GameState.Lose);
        PauseMenu.gameObject.SetActive(state == GameState.Pause);
        LoseMenu.gameObject.SetActive(state == GameState.Lose);

        if (state == GameState.Lose)
        {
            int score = PlayerControl.Instance.score;
            totalScoreText.text = "Your score: " + score;
            CheckHighscore(score);
        }
        if (state == GameState.Restart)
        {
            GameManager.UpdateState -= UIGameplayOnStateChange;
        }
    }

    void CheckHighscore(int score)
    {
        Array.Copy(GetPlayerData(), highscores, GetPlayerData().Length);
        if (score > highscores[highscores.Length - 1].score)
        {
            InputField.gameObject.SetActive(true);
            playerHighscore = score;
        }
    }
    PlayerData[] GetPlayerData()
    {
        // This prevents players who want to delete highscores file when beat the hall of fame (i mean highscore table)
        // yeah yeah I know they can do stuff with my game and the saved files too but this is the only thing i can do
        // and that make me feel like I'm GENIUS about security
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonHelper.FromJson<PlayerData>(json);

        }
        else
        {
            PlayerData[] highscoresTemp = new PlayerData[10];
            for (int i = 0; i < highscoresTemp.Length; i++)
            {
                int num = i + 1;
                highscoresTemp[i] = new PlayerData("BOT " + num, Random.Range(100, 10000));
            }
            var qry = from p in highscoresTemp
                      orderby p.score descending
                      select p;
            string json = JsonHelper.ToJson(qry.ToArray<PlayerData>(), true);
            File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
            return JsonHelper.FromJson<PlayerData>(json);
        }
    }
    public void InvokeCountDown()
    {
        countDownText.gameObject.SetActive(true);
        time = 3;
        countDownText.text = time.ToString();
        InvokeRepeating(nameof(CountDown), 1, 1);
    }

    void CountDown()
    {
        time--;
        countDownText.text = time.ToString();
        if (time == 0)
        {
            countDownText.gameObject.SetActive(false);
            CancelInvoke(nameof(CountDown));
            GameManager.Instance.HandleState(GameState.Continue);
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score : " + (score == 0 ? 0 : score);
    }

    public void SetLife(int lifeLeft)
    {
        LifeText.text = "Lifes: " + lifeLeft;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void SetHighscore()
    {
        if (playerName.Length == 0)
        {
            playerName = "Tipha";
        }
        PlayerData newPlayer = new PlayerData(playerName, playerHighscore);
        highscores[highscores.Length - 1] = newPlayer;
        var qry = from p in highscores
                  orderby p.score descending
                  select p;
        string json = JsonHelper.ToJson(qry.ToArray<PlayerData>(), true);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);


        InputField.gameObject.SetActive(false);
    }
}
