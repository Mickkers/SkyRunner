using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private RectTransform pauseUI;

    [Header("Gameover Menu")]
    [SerializeField] private RectTransform gameoverUI;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private HighScoreManager highScoreManager;
    private GameManager gameManager;
    private AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        highScoreManager = FindObjectOfType(typeof(HighScoreManager)) as HighScoreManager;
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        audioController.GameoverSFX();
        Time.timeScale = 0;
        gameoverUI.gameObject.SetActive(true);
        finalScore.text = "Score: " + (int)gameManager.gameScore;
        if(highScoreManager.gameData.highScores[0] == (int)gameManager.gameScore)
        {
            highScore.text = "New High Score";
        }
        else
        {
            highScore.text = "High Score: " + highScoreManager.gameData.highScores[0];
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
