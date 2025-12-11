using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu UI interactions including Play, Settings, and Quit buttons.
/// Manages scene transitions and user preferences.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    /// <summary>
    /// Reference to the Settings panel UI that can be toggled on/off.
    /// </summary>
    public GameObject settingsPanel;

    /// <summary>
    /// Called when the Play button is pressed.
    /// Resumes from last played level if available, otherwise starts a new game.
    /// </summary>
    public void OnPlayButton()
    {
        // Check if the player was previously in a level (saved by ESC)
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            // Load the last played level (e.g., MainLevel2, MainLevel3, etc.)
            string level = PlayerPrefs.GetString("LastLevel");
            SceneManager.LoadScene(level);
        }
        else
        {
            // If no level was saved, this is first-time play → start main level
            SceneManager.LoadScene("MainLevel");
        }
    }

    /// <summary>
    /// Called when the Settings button is pressed.
    /// Displays the settings panel to the player.
    /// </summary>
    public void OnSettingsButton()
    {
        // Open the settings panel (make it visible)
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Called when the Close button inside Settings is pressed.
    /// Hides the settings panel and returns to the main menu.
    /// </summary>
    public void OnCloseSettingsButton()
    {
        // Hide the settings panel
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Called when the Quit button is pressed.
    /// Exits the application (only works in the built game, not in the editor).
    /// </summary>
    public void OnQuitButton()
    {
        Debug.Log("Quit"); // Visible only in Editor for testing
        Application.Quit(); // Exits the application in the actual build
    }
}
