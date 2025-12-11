using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;

    public void OnPlayButton()
    {
        SceneManager.LoadScene("MainLevel"); 
    }

    public void OnSettingsButton()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OnCloseSettingsButton()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit"); // just to test in Editor
        Application.Quit();
    }
}
