using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool musicOn = false;
    public bool gameOver = false;
    public int score = 0;
    public GameObject mainPanel;
    public GameObject pausePanel;
    public GameObject gamePanel;
    public GameObject overPanel;
    public Image musicSprite;
    public Image pauseMusicSprite;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Text gameScoreText;
    public Text overScoreText;

    public static GameManager Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    
    void Start()
    {
        musicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        musicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        pauseMusicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        AudioManager.Instance.Mute(!musicOn);
    }
    
    void Update()
    {
        
    }

    public void StartButton()
    {
        AudioManager.Instance.StopMainMenuMusic();
        AudioManager.Instance.PlayInGameMusic();
        AudioManager.Instance.PlaySound("UIClick");
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        overPanel.SetActive(false);
    }

    public void MusicButton()
    {
        musicOn = !musicOn;
        musicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        pauseMusicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        AudioManager.Instance.Mute(!musicOn);
        PlayerPrefs.SetInt("Music", musicOn ? 1 : 0);
        AudioManager.Instance.PlaySound("UIClick");
    }

    public void PauseButton()
    {
        AudioManager.Instance.PlaySound("UIOpen");
        mainPanel.SetActive(false);
        pausePanel.SetActive(true);
        gamePanel.SetActive(false);
        overPanel.SetActive(false);
    }

    public void HomeButton()
    {
        AudioManager.Instance.StopInGameMusic();
        AudioManager.Instance.PlayMainMenuMusic();
        AudioManager.Instance.PlaySound("UIClick");
        gameOver = false;
        ScoreChange(0);
        mainPanel.SetActive(true);
        pausePanel.SetActive(false);
        gamePanel.SetActive(false);
        overPanel.SetActive(false);
        AudioManager.Instance.PlaySound("UIOpen");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySound("UIClick");
        gameOver = false;
        ScoreChange(0);
    }

    public void UnPauseButton()
    {
        AudioManager.Instance.PlaySound("UIClose");
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        overPanel.SetActive(false);
    }

    public void ScoreChange(int newScore)
    {
        score = newScore;
        gameScoreText.text = score.ToString();
        overScoreText.text = score.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
    }
}
