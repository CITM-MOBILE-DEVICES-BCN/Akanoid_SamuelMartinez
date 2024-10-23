using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class GameData
{
    public int score;
    public int highScore;
    public int lifes;
    public int level;
    public List<ObstacleData> obstaclesLeft;

    public GameData()
    {
        score = 0;
        highScore = 0;
        lifes = 3;
        level = 0;
        obstaclesLeft = new List<ObstacleData>();
    }

}

public class GameFacade
{
    public GameData gameData;
    public ScoreManager scoreManager;
    public SaveAndLoadManager saveAndLoad;
    public void Init()
    {
        saveAndLoad = new SaveAndLoadManager();
        gameData = new GameData();
        LoadGame();
    }

    public void InitGame(bool isNewGame)
    {
        if (isNewGame) 
        {
            scoreManager = new ScoreManager(gameData.highScore);
            gameData = new GameData();
            ResetData();
        }
        else 
        { 
            scoreManager = new ScoreManager(gameData); 
        }
    }
    public void ObstacleDestroyed()
    {
        scoreManager.UpdateScore();
    }
    public void ResetData()
    {
        saveAndLoad.ResetGame();
    }
    public bool isPlayerWithoutLifes()
    {
       scoreManager.SubtractLife();
        if (scoreManager.numberOfLifes <= 0)
        {
            scoreManager.ResetPlayer();
            return true;
        }
        else 
        { 
            return false;
        }

    }
    public void LevelUp()
    {
        scoreManager.level++;
    }
    public void SaveGame()
    {
        SetDataFromScoreManager();
        saveAndLoad.SaveInJSONfile(gameData);
    }
    public void SaveOnlyHighScore()
    {
        saveAndLoad.SaveHighScoreInJSONfile(gameData);
    }
    public void LoadGame()
    {
        gameData = saveAndLoad.LoadFromJSONFile();
    }
    public int GetSavedLevel()
    {
        return gameData.level;
    }
    public int GetCurrentLevel()
    {
        return scoreManager.level;
    }
    public void SetDataFromScoreManager()
    {
        gameData.score = scoreManager.score;
        gameData.highScore = scoreManager.highScore;
        gameData.lifes = scoreManager.numberOfLifes;
        gameData.level = scoreManager.level;
    }
}

    
