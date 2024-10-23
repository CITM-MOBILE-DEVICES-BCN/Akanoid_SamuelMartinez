using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highSocreText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private BarController barPrefab;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private ScreenLimitsAdjuster screenLimits;
    [SerializeField] private GameObject pauseScene;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void Start()
    {
        instance = this;
        slider.onValueChanged.AddListener(barPrefab.AdjustBarPosition);
        canvasController.Init(screenLimits.AdjustColliderSize);
        pauseScene.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = GameManager.instance.facade.scoreManager.score.ToString();
        highSocreText.text = GameManager.instance.facade.scoreManager.highScore.ToString();
        lifesText.text = GameManager.instance.facade.scoreManager.numberOfLifes.ToString();
    }

    public void SetSlider(bool condition)
    {
        slider.gameObject.SetActive(condition);
    }

    public void SetPauseScene(bool condition)
    {
        pauseScene.gameObject.SetActive(condition);
    }
    
    public void SetWinSceneActive()
    {
        winScreen.gameObject.SetActive(true);
    }

    public void SetLoseScene()
    {
        loseScreen.gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        GameManager.instance.ContinueGame();
    }
    public void QuitGame()
    {
        GameManager.instance.facade.SaveOnlyHighScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void NextLevel()
    {
        GameManager.instance.facade.LevelUp();
        GameManager.instance.facade.SaveGame();
        SceneManager.LoadScene(GameManager.instance.facade.GetSavedLevel());
    }
}

    

