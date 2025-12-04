using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Reference to the UI Text component
    private Text scoreText;

    void Start()
    {
        // Get the Text component attached to this GameObject
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        // Update the score display every frame
        scoreText.text = "Score: " + PlayerController.score;
    }
}