using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform highScoreMenu;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private RectTransform settingsMenu;

    private HighScoreManager highScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        highScoreManager = FindObjectOfType(typeof(HighScoreManager)) as HighScoreManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void EnableMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }

    private void DisableMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }

    public void EnableHighScores()
    {
        highScoreMenu.gameObject.SetActive(true);
        DisableMainMenu();
        SetHighScores();
    }

    public void DisableHighScores()
    {
        highScoreMenu.gameObject.SetActive(false);
        EnableMainMenu();
    }

    public void EnableSettings()
    {
        settingsMenu.gameObject.SetActive(true);
        DisableMainMenu();
    }

    public void DisableSettings()
    {
        settingsMenu.gameObject.SetActive(false);
        EnableMainMenu();
    }

    private void SetHighScores()
    {
        string text = "";
        int[] highScores = highScoreManager.GetHighScores();
        for(int i = 0; i < highScores.Length; i++)
        {
            if(i != 0)
            {
                text += "\n";
            }

            if(highScores[i] <= 0)
            {

                text += "-";
            }
            else
            {
                text += i + 1 + ". " + highScores[i];
            }
        }
        highScoreText.text = text;
    }
}
