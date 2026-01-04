using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missesText;
    public GameObject gameOverPanel;

    [Header("Game Rules")]
    public int maxMisses = 3;

    int score = 0;
    int misses = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void AddMiss()
    {
        misses++;
        UpdateUI();

        if (misses >= maxMisses)
            GameOver();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        missesText.text = "Misses: " + misses + " / " + maxMisses;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
