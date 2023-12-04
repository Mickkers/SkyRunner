using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private HighScoreManager highScoreManager;
    private GameplayUI gameplayUI;

    [SerializeField] private float gameSpeedIncrease;
    [SerializeField] private float gameScoreIncrease;

    [SerializeField] private Platform platformPrefab;
    [SerializeField] private float platformCD;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float enemyCD;

    [SerializeField] private TextMeshProUGUI scoreText;

    public float gameSpeed;
    public float gameScore;
    public bool gameActive;

    private bool canSpawnPlatform;
    private bool canSpawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        highScoreManager = FindObjectOfType(typeof(HighScoreManager)) as HighScoreManager;
        gameplayUI = FindObjectOfType(typeof(GameplayUI)) as GameplayUI;
        gameActive = true;
        canSpawnEnemy = true;
        canSpawnPlatform = true;
    }

    private void Update()
    {
        if (gameActive)
        {
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            gameScore += gameScoreIncrease * gameSpeed * 0.5f * Time.deltaTime;
        }
        UpdateScoreUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSpawnEnemy)
        {
            StartCoroutine(SpawnEnemy());
        }
        if (canSpawnPlatform)
        {
            StartCoroutine(SpawnPlatform());
        }

    }

    public void Gameover()
    {
        if (gameActive)
        {
            gameActive = false;
            highScoreManager.NewScore((int)gameScore);
            gameplayUI.GameOver();
        }
    }

    public void AddScore(float value)
    {
        gameScore += value;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + (int)gameScore;
    }

    public IEnumerator SpawnPlatform()
    {
        canSpawnPlatform = false;
        Platform platform = Instantiate(platformPrefab, new Vector3(10f, 1.5f + (float)Random.Range(0, 2) * -3f, 0), Quaternion.identity);

        yield return new WaitForSeconds(platformCD / gameSpeed);

        canSpawnPlatform = true;
    }

    public IEnumerator SpawnEnemy()
    {
        canSpawnEnemy = false;
        Enemy enemy = Instantiate(enemyPrefab, new Vector3(10f, 0f + Random.Range(-2.6f, 3.5f), 0), Quaternion.identity);

        yield return new WaitForSeconds(enemyCD / gameSpeed);

        canSpawnEnemy = true;
    }
}
