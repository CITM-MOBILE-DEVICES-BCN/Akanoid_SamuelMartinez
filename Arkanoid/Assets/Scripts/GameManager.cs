using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public SoundManager soundManager;
    public GameFacade facade;
    private bool isPaused = false;
    public int numLevels = 2;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        facade = new GameFacade();
        facade.Init();
        soundManager.PlayMenuMusic();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        facade.InitGame(true);
        facade.LevelUp();
        soundManager.StopMenuMusic();
        soundManager.PlayGameplayMusic();
    }
    public void ContinueGame()
    {
      
        Time.timeScale = 1;
        facade.InitGame(false);
        var level = facade.GetSavedLevel();
        if(level% (numLevels) == 0)
        {
            level = numLevels;
        }
        else
        {
            level = level % (numLevels);
        }
        SceneManager.LoadScene(level);
        facade.SaveGame();
        soundManager.StopMenuMusic();
        soundManager.PlayGameplayMusic();
    }
    public void SetPause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        UiController.instance.SetPauseScene(isPaused);
    }

    public bool CheckPlayerLifes()
    {
        if(facade.isPlayerWithoutLifes())
        {
            return false;
        }
        else 
        { 
            return true; 
        }
    }

    public void SetGameOverScene()
    {
        soundManager.PlayDeathSoundEffect();
        GameState.currentState = GameState.State.GameOver;
        Time.timeScale = 0;
        facade.ResetData();
        // Llamar a la función del UI Controller
        UiController.instance.SetLoseScene();
    }
    public void SetWinScene()
    {
        soundManager.PlayWinSoundEffect();
        facade.LevelUp();
        facade.SaveGame();
        GameState.currentState = GameState.State.GameOver;      
        Time.timeScale = 0;
        UiController.instance.SetWinSceneActive();
    }

  
}
