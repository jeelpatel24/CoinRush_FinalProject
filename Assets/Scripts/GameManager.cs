using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the overall game state, score tracking, and UI updates.
/// Implements the Singleton pattern to ensure only one GameManager exists globally.
/// Handles win/lose conditions and level progression.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Static reference to the single GameManager instance (Singleton).
    /// Allows other scripts to access GameManager globally via GameManager.Instance.
    /// </summary>
    public static GameManager Instance;

    // ------------------------------
    // UI Elements
    // ------------------------------
    /// <summary>
    /// Reference to the Text UI component that displays the player's total score.
    /// </summary>
    [Header("UI References")]
    [SerializeField] private Text scoreText;

    /// <summary>
    /// Reference to the Text UI component that displays the number of collected crystals.
    /// </summary>
    [SerializeField] private Text crystalsText;

    /// <summary>
    /// Reference to the Text UI component that displays the player's health as heart symbols.
    /// </summary>
    [SerializeField] private Text healthText;

    /// <summary>
    /// Reference to the GameObject that contains the win screen UI.
    /// Shown when the player successfully completes the level.
    /// </summary>
    [SerializeField] private GameObject winPanel;

    /// <summary>
    /// Reference to the GameObject that contains the game over screen UI.
    /// Shown when the player's health reaches zero.
    /// </summary>
    [SerializeField] private GameObject gameOverPanel;

    // ------------------------------
    // Game Stats
    // ------------------------------
    /// <summary>
    /// The total number of crystals required to complete the level.
    /// </summary>
    [Header("Game Stats")]
    [SerializeField] private int totalCrystals = 5;

    /// <summary>
    /// Tracks the player's total score (incremented by crystal collection).
    /// </summary>
    private int score = 0;

    /// <summary>
    /// Tracks the number of crystals the player has collected.
    /// </summary>
    private int crystalsCollected = 0;

    // ------------------------------
    // Awake - Singleton Setup
    // ------------------------------
    /// <summary>
    /// Called when the script instance is being loaded (before Start).
    /// Sets up the Singleton pattern to ensure only one GameManager exists.
    /// </summary>
    void Awake()
    {
        // Ensure only ONE GameManager exists at a time
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates if scene reloads
        }
    }

    // ------------------------------
    // Start - Initialize UI & Panels
    // ------------------------------
    /// <summary>
    /// Called on the first frame when the script is enabled.
    /// Initializes the UI display and hides win/lose panels.
    /// </summary>
    void Start()
    {
        UpdateUI();

        // Hide win/lose screens at the start of the level
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    // ------------------------------
    // Add Score (called by Crystal script)
    // ------------------------------
    /// <summary>
    /// Adds points to the player's score when a crystal is collected.
    /// Called by the Crystal script when the player collects a crystal.
    /// </summary>
    /// <param name="points">The number of points to add to the score.</param>
    public void AddScore(int points)
    {
        score += points;          // Increase score
        crystalsCollected++;      // Count crystal collection

        UpdateUI();               // Update UI labels
    }

    // ------------------------------
    // Update the Score + Crystal UI
    // ------------------------------
    /// <summary>
    /// Updates the score and crystals UI text with current values.
    /// </summary>
    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (crystalsText) crystalsText.text = "Crystals: " + crystalsCollected + "/" + totalCrystals;
    }

    // ------------------------------
    // Update Player Health UI (Hearts ♥)
    // ------------------------------
    /// <summary>
    /// Updates the health UI display with heart symbols based on current health.
    /// Called by the PlayerController when health changes.
    /// </summary>
    /// <param name="currentHealth">The player's current health value.</param>
    /// <param name="maxHealth">The player's maximum health value.</param>
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText)
        {
            // Optimization: Use switch or StringBuilder for repeated updates
            string hearts = "";
            for (int i = 0; i < currentHealth; i++)
            {
                hearts += "♥ ";
            }
            healthText.text = "Health: " + hearts;
        }
    }

    // ------------------------------
    // Win the game (called when level is completed)
    // ------------------------------
    /// <summary>
    /// Called when the player successfully completes the level by entering the exit portal.
    /// Pauses the game and displays the win screen.
    /// </summary>
    public void WinGame()
    {
        Time.timeScale = 0f;           // Pause the game
        if (winPanel) winPanel.SetActive(true);
    }

    // ------------------------------
    // Lose the game (player dies)
    // ------------------------------
    /// <summary>
    /// Called when the player's health reaches zero.
    /// Pauses the game and displays the game over screen.
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0f;           // Pause everything
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    // ------------------------------
    // Return how many crystals player collected
    // ------------------------------
    /// <summary>
    /// Returns the total number of crystals collected by the player.
    /// Used by ExitPortal to determine if the player can exit.
    /// </summary>
    /// <returns>The number of crystals collected.</returns>
    public int GetCrystalsCollected()
    {
        return crystalsCollected;
    }

    // ------------------------------
    // Restart the current level
    // ------------------------------
    /// <summary>
    /// Reloads the current level, resetting all game progress.
    /// Called when the player clicks the restart button on the game over screen.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ------------------------------
    // Load the next level in build order
    // ------------------------------
    /// <summary>
    /// Loads the next level from the Build Settings scene list.
    /// Called when the player clicks the next level button on the win screen.
    /// If no more levels exist, loads the home menu.
    /// </summary>
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // If next level exists in Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // No more levels — game complete!
            Debug.Log("Game Complete! All levels finished!");
            SceneManager.LoadScene(0); // Load first scene (Home/Menu)
        }
    }
}
