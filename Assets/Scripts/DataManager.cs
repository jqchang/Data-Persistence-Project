using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public HighScoreTable highScores;

    // Start is called before the first frame update
    void Awake() {
        if(Instance == null) {
            Instance = this;
            highScores = new HighScoreTable();
            LoadHighScores();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void SaveHighScores()
    {
        HighScoreTable data = new HighScoreTable();
        data.scoreList = highScores.scoreList;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreTable data = JsonUtility.FromJson<HighScoreTable>(json);
            highScores.scoreList = data.scoreList;
        } else {
            highScores = new HighScoreTable();
            highScores.scoreList = new List<HighScore>();
        }
    }

    public HighScore HighestScore()
    {
        if(highScores.scoreList.Count == 0) {
            HighScore empty = new HighScore();
            empty.playerName = "(none)";
            empty.score = 0;
            return empty;
        } else {
            return highScores.scoreList[0];
        }
    }

    public void AddScore(HighScore score)
    {
        highScores.scoreList.Add(score);
        highScores.scoreList.Sort(delegate(HighScore x, HighScore y) {
            if(x.score > y.score) return -1;
            else return 1;
        });
        if(highScores.scoreList.Count > 5) {
            highScores.scoreList = highScores.scoreList.GetRange(0,5);
        }
        Debug.Log(highScores.scoreList.Count);
    }

    public void ResetScoreTable()
    {
        highScores.scoreList = new List<HighScore>();
    }

    [System.Serializable]
    public class HighScore
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class HighScoreTable
    {
        public List<HighScore> scoreList;
    }
}
