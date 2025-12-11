using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text crystalsText;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Stats")]
    [SerializeField] private int totalCrystals = 5;

    private int score = 0;
    private int crystalsCollected = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();

        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        Debug.Log("GameManager Started!");
    }

    public void AddScore(int points)
    {
        score += points;
        crystalsCollected++;
        UpdateUI();

        Debug.Log("Score: " + score + " | Crystals: " + crystalsCollected + "/" + totalCrystals);
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (crystalsText) crystalsText.text = "Crystals: " + crystalsCollected + "/" + totalCrystals;
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText)
        {
            // Display hearts
            string hearts = "";
            for (int i = 0; i < currentHealth; i++)
            {
                hearts += "♥ ";
            }
            healthText.text = "Health: " + hearts;
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        if (winPanel) winPanel.SetActive(true);
        Debug.Log("YOU WIN!");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Debug.Log("GAME OVER!");
    }

    public int GetCrystalsCollected()
    {
        return crystalsCollected;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there's a next level
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // No more levels - go back to first level or show credits
            Debug.Log("Game Complete! All levels finished!");
            SceneManager.LoadScene(0); // Load first level
        }
    }
}