using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveAndLoadManager
{
    public void SaveInJSONfile(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    } 
  public void SaveHighScoreInJSONfile(GameData gameData)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData existingData = JsonUtility.FromJson<GameData>(json);
            if (gameData.highScore > existingData.highScore)
            {
                existingData.highScore = gameData.highScore;
                json = JsonUtility.ToJson(existingData);
                File.WriteAllText(path, json);
            }
        }
        else
        {
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(path, json);
        }
    }

    public GameData LoadFromJSONFile()
    {
        GameData data = new GameData()
        {
            score = 0,
            highScore = 0,
            lifes = 3,
            level = 0,
            obstaclesLeft = new List<ObstacleData>()
        };
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<GameData>(json);
        }
        return data;
    }

    public void ResetGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            data.score = 0;
            data.lifes = 3;
            data.level = 0;
            data.obstaclesLeft = new List<ObstacleData>();
            json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
    }
}
