using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Called when Play button is clicked
    public void PlayGame()
    {
        // Load the Level 1 scene
        SceneManager.LoadScene("Level1");
    }

    // Called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        // This line is only for testing in Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Called to restart current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called to return to main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}