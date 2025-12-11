using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;      // Horizontal movement speed
    [SerializeField] private float jumpForce = 17f;     // Upward force applied when jumping

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;      // Position where ground check is performed
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of circle used for ground detection
    [SerializeField] private LayerMask groundLayer;      // What counts as ground

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;          // Maximum player health

    private Rigidbody2D rb;                              // Reference to Rigidbody2D
    private bool isGrounded;                             // True if player is touching the ground
    private float moveInput;                             // Raw horizontal input value (A/D or arrows)
    private int currentHealth;                           // Current health value

    private bool isInvincible = false;                   // Whether the player can take damage
    private float invincibilityTime = 1f;                // Duration of invincibility after taking hit
    private float invincibilityTimer = 0f;               // Countdown timer for invincibility

    private SpriteRenderer spriteRenderer;               // Used for flashing effect during invincibility

    private AudioSource jumpAudioSource;                 // Audio source for jump sound
    private AudioSource damageAudioSource;               // Audio source for damage sound

    void Start()
    {
        // Cache components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get audio sources (first for jump, second for damage)
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 1) jumpAudioSource = audioSources[0];
        if (audioSources.Length >= 2) damageAudioSource = audioSources[1];

        // Initialize health
        currentHealth = maxHealth;

        // Update the UI at game start
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    void Update()
    {
        // Read raw horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded using overlap circle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Play jump sound (if not muted)
            if (jumpAudioSource != null && jumpAudioSource.clip != null)
            {
                if (AudioManager.Instance == null || !AudioManager.Instance.isSfxMuted)
                    jumpAudioSource.Play();
            }
        }

        // Handle invincibility timer + flashing effect
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            // Flash sprite transparency while invincible
            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            // End of invincibility
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;

                // Reset alpha to fully visible
                Color resetColor = spriteRenderer.color;
                resetColor.a = 1f;
                spriteRenderer.color = resetColor;
            }
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite to match movement direction
        if (moveInput > 0)       spriteRenderer.flipX = false; // face right
        else if (moveInput < 0)  spriteRenderer.flipX = true;  // face left
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name +
                  " | Tag: " + collision.gameObject.tag +
                  " | Invincible: " + isInvincible);

        // Take damage when colliding with enemy if not invincible
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play damage sound (if not muted)
        if (damageAudioSource != null && damageAudioSource.clip != null)
        {
            if (AudioManager.Instance == null || !AudioManager.Instance.isSfxMuted)
                damageAudioSource.Play();
        }

        // Update UI
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }

        Debug.Log("Player Health: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            // Handle game-over
            GameManager.Instance.GameOver();
        }
        else
        {
            // Activate temporary invincibility
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    // Draw ground check circle in editor for debugging
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
