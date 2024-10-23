using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BoosterController;

public class BoardController : MonoBehaviour
{
    public ObstacleController obstaclePrefab;
    public BoosterController boosterPrefab;
    public BarController barPrefab;
    public BallController ballPrefab;
    public List<ObstacleConfig> configs;
    public List<ObstacleController> allObstacles;
    public List<BallController> allBalls;
    public GameObject obstacleInstantiator;

    public Vector2 spaceBetweenPlatforms;
    public int numObstacles;

    // Start is called before the first frame update
    void Start()
    {
        allObstacles = new List<ObstacleController>();
        if (GameManager.instance.facade.gameData.obstaclesLeft.Count != 0)
        {
            for (int i = 0; i < GameManager.instance.facade.gameData.obstaclesLeft.Count; i++)
            {
                var obstacle = Instantiate(obstaclePrefab, obstacleInstantiator.transform);
                obstacle.Init(GameManager.instance.facade.gameData.obstaclesLeft[i], InstantiateBooster);
                allObstacles.Add(obstacle);

            }
        }
        else
        {
            for (int i = 0; i < numObstacles; i++)
            {
                var r = Random.Range(0, configs.Count);
                var obstacle = Instantiate(obstaclePrefab, obstacleInstantiator.transform);
                obstacle.Init(configs[r], InstantiateBooster);
                allObstacles.Add(obstacle);
            }
        }
        StartCoroutine(InvokeFirstBall());
    }

    private void Update()
    {
        if(GameState.currentState == GameState.State.Playing)
        {

            if (Input.GetKeyDown(KeyCode.A) && !barPrefab.isFollowingABall)
            {
                barPrefab.isFollowingABall = true;
                barPrefab.ballToFollowPosition = allBalls[0].GetComponent<RectTransform>();
                UiController.instance.SetSlider(false);
            }
            else if (Input.GetKeyDown(KeyCode.Q) && barPrefab.isFollowingABall)
            {
                barPrefab.isFollowingABall = false;
                UiController.instance.SetSlider(true);
            }        
        }        
    }

    public IEnumerator InvokeFirstBall()
    {
        yield return new WaitForSeconds(2f);
        InstantiateBall();
    }

    public void InstantiateBall()
    {
        var newBall = Instantiate(ballPrefab, this.transform);
        RectTransform ballRectTransform = newBall.GetComponent<RectTransform>();
        RectTransform barRectTransform = barPrefab.GetComponent<RectTransform>();
        ballRectTransform.position = new Vector2(barRectTransform.position.x, barRectTransform.position.y + 50);
        newBall.Init(UpdateList);
        allBalls.Add(newBall);
    }

    public void CheckAllBallsOnScene()
    {
        if (allBalls.Count == 0)
        {
            if(GameManager.instance.facade.isPlayerWithoutLifes())
            {
                GameManager.instance.SetGameOverScene();
            }
            else
            {
                StartCoroutine(InvokeFirstBall());

            }
        }
    }

    public void UpdateList(BallController ball)
    {
        allBalls.Remove(ball);
        CheckAllBallsOnScene();
    }

    public void InstantiateBooster(Vector2 spawnPosition, BoosterConfig boosterConfig, ObstacleController obstacle)
    {
        allObstacles.Remove(obstacle);
        if (allObstacles.Count != 0)
        {
            if (boosterConfig != null)
            {
                var booster = Instantiate(boosterPrefab, this.transform);
                RectTransform rectTransform = booster.GetComponent<RectTransform>();
                rectTransform.position = spawnPosition;
                booster.Init(boosterConfig, ActivateBooster);
            }
        }
        else
        {
            //You win code
            
            GameManager.instance.SetWinScene();
        }
            
    }

    public void ActivateBooster(boostertype type)
    {
        switch (type)
        {
            case boostertype.SpawnBallsOnBar:
                Debug.Log("SpawnBallsOnBar");
                InstantiateBall();
                break;
            case boostertype.SpawnBalls:
                var currentBallsOnScreen = allBalls.Count;
                for (int i = 0; i < currentBallsOnScreen; i++)
                {
                    var newBall = Instantiate(ballPrefab, this.transform);
                    RectTransform newBallRectTransform = newBall.GetComponent<RectTransform>();
                    RectTransform existingBallRectTransform = allBalls[i].GetComponent<RectTransform>();
                    newBallRectTransform.position = new Vector2(existingBallRectTransform.position.x, existingBallRectTransform.position.y);
                    allBalls.Add(newBall);
                }
                break;
            case boostertype.ReduceVelocity:
                break;
            case boostertype.BiggerBar:
                break;
            case boostertype.None:
                break;
        }
    }

    public void ExitGame()
    {
        GameManager.instance.facade.gameData.obstaclesLeft.Clear();
        for (int i = 0; i < allObstacles.Count; i++)
        {
            GameManager.instance.facade.gameData.obstaclesLeft.Add(allObstacles[i].GetObstacleData());
        }
        GameManager.instance.facade.SaveGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
