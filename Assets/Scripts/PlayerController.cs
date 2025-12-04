using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Components
    private Rigidbody2D rb;
    private bool isGrounded = false;

    // Score tracking
    public static int score = 0;

    // UI References
    public GameObject winPanel;
    public Text finalScoreText;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        score = 0; // Reset score at start

        // Make sure win panel is hidden at start
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump when Space is pressed and player is on ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // Detect when player touches ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    // Detect when player collects coins or reaches finish
    void OnTriggerEnter2D(Collider2D other)
    {
        // If player touches a coin
        if (other.gameObject.name.Contains("Coin"))
        {
            score += 10; // Add 10 points
            Destroy(other.gameObject); // Remove the coin
            Debug.Log("Coin collected! Score: " + score);
        }

        // If player reaches finish flag
        if (other.gameObject.name == "FinishFlag")
        {
            Debug.Log("You Win! Final Score: " + score);
            ShowWinScreen();
        }
    }

    // Show the win screen
    void ShowWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + score;
            }

            // Stop player movement
            rb.linearVelocity = Vector2.zero;
            moveSpeed = 0;
        }
    }
}