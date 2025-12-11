using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance for global access
    public static GameManager Instance;

    [Header("UI References")]
    [SerializeField] private Text scoreText;          // UI text showing score
    [SerializeField] private Text crystalsText;       // UI text showing crystal progress
    [SerializeField] private Text healthText;         // UI text showing hearts
    [SerializeField] private GameObject winPanel;     // Panel shown when player wins
    [SerializeField] private GameObject gameOverPanel;// Panel shown when player dies

    [Header("Game Stats")]
    [SerializeField] private int totalCrystals = 5;   // Total number of crystals in the level

    private int score = 0;            // Player score
    private int crystalsCollected = 0;// How many crystals the player has collected

    void Awake()
    {
        // Create or enforce singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Prevent duplicates across scenes
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize UI values
        UpdateUI();

        // Make sure win/lose panels are hidden at start
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        Debug.Log("GameManager Started!");
    }

    // Add score and count crystals
    public void AddScore(int points)
    {
        score += points;
        crystalsCollected++;    // Each point assumes 1 crystal collected
        UpdateUI();

        Debug.Log("Score: " + score + " | Crystals: " 
                  + crystalsCollected + "/" + totalCrystals);
    }

    // Update score and crystal UI
    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (crystalsText) crystalsText.text = 
            "Crystals: " + crystalsCollected + "/" + totalCrystals;
    }

    // Update health UI (using hearts)
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText)
        {
            // Build heart-based display
            string hearts = "";
            for (int i = 0; i < currentHealth; i++)
            {
                hearts += "♥ ";
            }

            healthText.text = "Health: " + hearts;
        }
    }

    // Trigger win screen and freeze game
    public void WinGame()
    {
        Time.timeScale = 0f;
        if (winPanel) winPanel.SetActive(true);
        Debug.Log("YOU WIN!");
    }

    // Trigger game-over screen and freeze game
    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Debug.Log("GAME OVER!");
    }

    // Getter for collected crystals
    public int GetCrystalsCollected()
    {
        return crystalsCollected;
    }

    // Restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the next scene in Build Settings
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Load next scene if it exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // No more levels -> restart or show ending
            Debug.Log("Game Complete! All levels finished!");
            SceneManager.LoadScene(0); // Load first level again
        }
    }
}
