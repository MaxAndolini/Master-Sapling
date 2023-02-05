using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject rootMan;
    public GameObject tree;
    public int numberOfRoots = 2;
    public bool musicOn;
    public bool gameOver;
    public bool paused = false;
    public int health = 100;
    public int score;
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
    public Healthbar healthbar;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        tree = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        musicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        musicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        pauseMusicSprite.sprite = musicOn ? musicOnSprite : musicOffSprite;
        AudioManager.Instance.Mute(!musicOn);
        PauseGame();
        healthbar.UpdateBar(health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !paused)
            if (numberOfRoots != 0)
            {
                //rootman_sound.Play();
                var treePos = tree.transform.position;
                Instantiate(rootMan, new Vector3(treePos.x, treePos.y, treePos.z + 1.0f), Quaternion.identity);
                numberOfRoots--;
            }
    }

    public void StartButton()
    {
        AudioManager.Instance.StopMainMenuMusic();
        AudioManager.Instance.PlayInGameMusic();
        AudioManager.Instance.PlaySound("UIClick");
        ResumeGame();
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
        PauseGame();
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
        PauseGame();
        ScoreChange(0);
        mainPanel.SetActive(true);
        pausePanel.SetActive(false);
        gamePanel.SetActive(false);
        overPanel.SetActive(false);
        AudioManager.Instance.PlaySound("UIOpen");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlayInGameMusic();
        AudioManager.Instance.PlaySound("UIClick");
        gameOver = false;
        ScoreChange(0);
        ResumeGame();
    }

    public void UnPauseButton()
    {
        AudioManager.Instance.PlaySound("UIClose");
        ResumeGame();
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
    
    public void ScoreAdd(int newScore)
    {
        score += newScore;
        gameScoreText.text = score.ToString();
        overScoreText.text = score.ToString();
    }

    public void AddNumberOfRoots()
    {
        numberOfRoots++;
    }

    public void GameOver()
    {
        AudioManager.Instance.StopInGameMusic();
        AudioManager.Instance.PlaySound("GameOverUgh");
        PauseGame();
        gameOver = true;
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
    }
    
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void DecreaseHealth()
    {
        health -= 20;
        if (health > 0) healthbar.UpdateBar(health);
        else GameOver();
    }
}