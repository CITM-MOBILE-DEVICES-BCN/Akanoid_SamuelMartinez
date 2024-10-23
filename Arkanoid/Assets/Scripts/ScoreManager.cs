using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager
{
    public int score;
    public int highScore;
    public int numberOfLifes;
    public int level;
    public ScoreManager()
    {
        score = 0;
        highScore = 0;
        numberOfLifes = 3;
        level = 0;
    }
    public ScoreManager(GameData data)
    {
        score = data.score;
        highScore = data.highScore;
        numberOfLifes = data.lifes;
        level = data.level;
    }
    public ScoreManager(int highScore)
    {
        score = 0;
        this.highScore = highScore;
        numberOfLifes = 3;
        level = 0;
    }

    public void UpdateScore()
    {
        score += 1;
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void SubtractLife()
    {
        numberOfLifes--;
        
    }

    public void SetDefaultValues()
    {
        score = 0;
        highScore = 0;
        numberOfLifes = 3;
    }
    public void ResetPlayer()
    {
        score = 0;
        numberOfLifes = 3;
    }
}
