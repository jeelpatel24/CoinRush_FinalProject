using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles ESC key input to return to the Home menu from any game scene.
/// Also stores the current scene as the last played level for resume functionality.
/// </summary>
public class EscToHome : MonoBehaviour
{
    /// <summary>
    /// Checks for ESC key input every frame and handles the scene transition.
    /// </summary>
    void Update()
    {
        // Check if the ESC key was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Get the name of the currently active scene
            string currentScene = SceneManager.GetActiveScene().name;

            // Save the current scene name to PlayerPrefs so players can resume from here later
            PlayerPrefs.SetString("LastLevel", currentScene);

            // Load the Home menu scene
            SceneManager.LoadScene("Home");
        }
    }
}
