using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ------------------------------
    // Movement Settings
    // ------------------------------
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   // Horizontal movement speed
    [SerializeField] private float jumpForce = 17f;  // Vertical jump force

    // ------------------------------
    // Ground Check Settings
    // ------------------------------
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;  // Point under player to detect ground
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of ground detection circle
    [SerializeField] private LayerMask groundLayer;  // Which layer counts as ground

    // ------------------------------
    // Health System
    // ------------------------------
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;      // Max health of the player

    private int currentHealth;                       // Current player health
    private bool isInvincible = false;               // Prevents taking damage repeatedly
    private float invincibilityTime = 1f;            // Duration of invincibility after damage
    private float invincibilityTimer = 0f;           // Timer countdown

    // ------------------------------
    // Cached Components
    // ------------------------------
    private Rigidbody2D rb;                          // Player physics body
    private SpriteRenderer spriteRenderer;           // Player sprite for flipping/flashing
    private AudioSource jumpAudioSource;             // Jump sound
    private AudioSource damageAudioSource;           // Damage sound

    // Input & State
    private bool isGrounded;                         // Is the player touching the ground?
    private float moveInput;                         // Left/right input (-1, 0, or 1)

    // ------------------------------
    // Initialization
    // ------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get audio sources from player (0 = jump, 1 = damage)
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 1) jumpAudioSource = audioSources[0];
        if (audioSources.Length >= 2) damageAudioSource = audioSources[1];

        currentHealth = maxHealth;

        // Update health UI at the start
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    // ------------------------------
    // Handle Input & Gameplay Logic
    // ------------------------------
    void Update()
    {
        // Get horizontal movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Detect ground using a small circle under the player
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump only when grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Play jump sound (only if SFX is not muted)
            if (jumpAudioSource != null && jumpAudioSource.clip != null)
            {
                if (AudioManager.Instance == null || !AudioManager.Instance.isSfxMuted)
                    jumpAudioSource.Play();
            }
        }

        // Handle temporary invincibility after being damaged
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            // Flashing effect (changes transparency) - only update if visible
            if (invincibilityTimer > 0f)
            {
                float alpha = Mathf.PingPong(Time.time * 10f, 1f);
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }

            // When invincibility ends, reset appearance
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;

                Color resetColor = spriteRenderer.color;
                resetColor.a = 1f;
                spriteRenderer.color = resetColor;
            }
        }
    }

    // ------------------------------
    // Physics-Based Movement
    // ------------------------------
    void FixedUpdate()
    {
        // Apply horizontal velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite direction depending on movement
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;  // Facing left
        }
    }

    // ------------------------------
    // Collision Handling
    // ------------------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Take damage if touching an enemy and not invincible
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    // ------------------------------
    // Apply Damage to Player
    // ------------------------------
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play damage sound (if SFX not muted)
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

        // If health is 0 → Game Over
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            // Otherwise become temporarily invincible to avoid instant death
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    // ------------------------------
    // Visual Helper in Editor
    // Draws the ground check circle in Scene View
    // ------------------------------
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}