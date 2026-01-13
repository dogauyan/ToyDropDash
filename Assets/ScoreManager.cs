using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missesText;
    public TextMeshProUGUI comboText;
    public GameObject gameOverPanel;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("High Score")]
    public TextMeshProUGUI highScoreText;

    [Header("Game Rules")]
    public int maxMisses = 3;

    int score = 0;
    int misses = 0;
    int combo = 0;
    int bestCombo = 0;
    int highScore = 0;

    bool isPaused = false;
    bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    // NORMAL / BONUS catch
    // 🔥 NOW RETURNS FINAL AWARDED SCORE
    public int AddScore(int basePoints)
    {
        combo++;
        bestCombo = Mathf.Max(bestCombo, combo);

        int finalPoints = basePoints * Mathf.Max(1, combo);
        score += finalPoints;

        UpdateUI();
        return finalPoints;
    }

    // NORMAL / BONUS miss
    public void AddMiss()
    {
        misses++;
        BreakCombo();
        UpdateUI();

        if (misses >= maxMisses)
            GameOver();
    }

    // TRAP catch
    public void BreakCombo()
    {
        combo = 0;
        UpdateUI();
    }

    public void ApplyTrapPenalty(int penalty)
    {
        BreakCombo();
        score = Mathf.Max(0, score - penalty);
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        missesText.text = $"Misses: {misses} / {maxMisses}";
        comboText.text = combo > 1 ? $"Combo x{combo}" : "";

        if (highScoreText != null)
            highScoreText.text = $"High Score: {highScore}";
    }

    void GameOver()
    {
        isGameOver = true;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        UpdateUI();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (isGameOver) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}