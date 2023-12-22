using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public int highScore = 0;
    public Text scoreText;
    public Text highScoreText;
    public Text microphoneText;
    public GameObject gameOverScreen;
    public bool gameOverBool = false;
    private AudioSource squeackSound;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Record: " + highScore.ToString();
        squeackSound = GetComponent<AudioSource>();
    }
    public void addScore(int scoreToAdd)
    {
        if (gameOverBool == false)
        {    
            playerScore = playerScore + scoreToAdd;
            scoreText.text = playerScore.ToString();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        squeackSound.Play();
        gameOverScreen.SetActive(true);
        gameOverBool = true;
        if (playerScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);        
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void setMicrophoneText(string text)
    {
        microphoneText.text = text;
    }

    public void restarRecord()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScoreText.text = "Record: 0";
    }
}
