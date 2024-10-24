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
    [SerializeField] private GameObject lifeIconPrefab;
    [SerializeField] private List<GameObject> lifeIcons;
    private int referenceScore;

    public void Start()
    {
        instance = this;
        lifeIcons = new List<GameObject>();
        slider.onValueChanged.AddListener(barPrefab.AdjustBarPosition);
        canvasController.Init(screenLimits.AdjustColliderSize);
        pauseScene.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
        for (int i = 0; i < GameManager.instance.facade.scoreManager.numberOfLifes; i++)
        {
            var lifeIcon = Instantiate(lifeIconPrefab, transform);
            lifeIcon.transform.position = new Vector3(lifeIcon.transform.position.x , lifeIcon.transform.position.y - i * 50, lifeIcon.transform.position.z);
            lifeIcons.Add(lifeIcon);
        }
        referenceScore = GameManager.instance.facade.scoreManager.numberOfLifes;
    }

    private void Update()
    {
        scoreText.text = GameManager.instance.facade.scoreManager.score.ToString();
        highSocreText.text = GameManager.instance.facade.scoreManager.highScore.ToString();
        var numberOfLifes = GameManager.instance.facade.scoreManager.numberOfLifes;
        if(referenceScore!= numberOfLifes)
        {
            referenceScore = numberOfLifes;
            if(referenceScore == 3)
            {
                referenceScore = 0;

            }
            DestroyLastLife();
        }
    }

    public void DestroyLastLife()
    {
        Destroy(lifeIcons[referenceScore]);        
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

    

