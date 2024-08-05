using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Engine : MonoBehaviour
{
    private UnityAdsManager UAM;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    TextMeshProUGUI lifesText;

    [SerializeField]
    TextMeshProUGUI timerText;
    public int score = 0;

    [SerializeField]
    int scoreToGetLife = 0; //score which is needed to get extra life
    public int LiveAdderForScore = 0; //counter for next extra life
    public int lifes = 3;

    [SerializeField]
    bool gameOver = false;

    float timer;
    float seconds;
    float minutes;

    public bool isPaused = false;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject gameOverMenu;

    public static Engine engine;

    private const string HighScoreKey = "HighScore";

    void Start()
    {
        engine = this;
        timer = 0;
        UAM = gameObject.AddComponent<UnityAdsManager>();
        DisplayHighScore();
    }

    private void DisplayHighScore()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = highScore.ToString();
        Debug.Log("HighScore : " + highScore.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (lifes < 1)
        {
            lifesText.text = "";
        }
        else
        {
            lifesText.text = lifes.ToString();
        }

        StopWatchCalc(); //calculation for stopwatch

        //pause menu system
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; //pause time when pause active
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //enables pause when esc presed
        {
            ChangePauseState();
        }

        //adds life if extra life counter is greater than var of getting extra life
        // if (LiveAdderForScore >= scoreToGetLife)
        // {
        //     lifes++;
        //     LiveAdderForScore = 0;
        // }

        if (gameOver)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(false);
        }

        if (gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (lifes < 1)
        {
            gameOver = true;
        }

        // Check if the current score is higher than the high score
        if (score > PlayerPrefs.GetInt(HighScoreKey, 0))
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();

            // Display the high score
            DisplayHighScore();
        }
    }

    //stopwatch system which count play time
    void StopWatchCalc()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;
        minutes = (int)timer / 60;
        if (seconds < 10)
        {
            timerText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timerText.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    public void ChangePauseState()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }

    public void Restart()
    {
        Debug.Log("Is Callled restart");
        SceneManager.LoadScene(2);
    }

    public void WatchAd()
    {
        Debug.Log("Ad Watching...");
        UAM.ShowRewardedVideo();
    }

    public void addExtraLife()
    {
        lifes = 1;
    }

    public void closeGameOverMenu()
    {
        Debug.Log("Closing gameOverMenu");
        gameOverMenu.SetActive(false);
        gameOver = false; // Reset gameOver state
    }
}