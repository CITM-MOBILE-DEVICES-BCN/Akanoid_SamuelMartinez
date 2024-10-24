using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum State
    {
        Playing,
        Paused,
        GameOver
    }

    public static State currentState;

    void Start()
    {
        currentState = State.Playing;
        Debug.Log("Game State: Start");
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Playing:
                // Handle Playing state
               
                if (Input.GetKeyDown(KeyCode.P))
                {
                    currentState = State.Paused;
                    GameManager.instance.SetPause();
                    GameManager.instance.soundManager.PlayPauseSoundEffect();
                    Debug.Log("Game State: Paused");
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //AutoWin
                    GameManager.instance.SetWinScene();
                } 
                if (Input.GetKeyDown(KeyCode.L))
                {
                    //AutoWin
                    GameManager.instance.SetGameOverScene();
                }
                break;

            case State.Paused:
                // Handle Paused state
                if (Input.GetKeyDown(KeyCode.P))
                {
                    GameManager.instance.soundManager.PlayPauseSoundEffect();
                    currentState = State.Playing;
                    GameManager.instance.SetPause();
                    Debug.Log("Game State: Playing");
                }
                break;
            case State.GameOver:
                break;
        }
    }
}
