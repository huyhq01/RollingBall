using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour
{
    private Transform Content;
    private Transform Template;
    public PlayerData[] highscores { get; private set;}
    private float rowHeight = 50f;
    private void Awake()
    {
        Content = transform.Find("Content");
        Template = Content.Find("Template");

        Template.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetHighscore();
    }

    void GetHighscore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LoadHighScore(json);
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
            GetHighscore();
        }

    }

    void LoadHighScore(string jsonString)
    {
        PlayerData[] players = JsonHelper.FromJson<PlayerData>(jsonString);
        for (int i = 0; i < players.Length; i++)
        {
            CreateHighscoreTransform(players[i], Content, i + 1);
        }
    }

    private void CreateHighscoreTransform(PlayerData playerData, Transform container, int order)
    {
        Transform entryTransform = Instantiate(Template, container);
        RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -rowHeight * order);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("rank").GetComponent<Text>().text = (order).ToString();
        entryTransform.Find("name").GetComponent<Text>().text = playerData.name;
        entryTransform.Find("score").GetComponent<Text>().text = playerData.score.ToString();
    }

    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }
}