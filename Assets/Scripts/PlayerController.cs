using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 17f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private int currentHealth;
    private bool isInvincible = false;
    private float invincibilityTime = 1f;
    private float invincibilityTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private AudioSource jumpAudioSource;
    private AudioSource damageAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get both audio sources
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 1) jumpAudioSource = audioSources[0];
        if (audioSources.Length >= 2) damageAudioSource = audioSources[1];

        currentHealth = maxHealth;

        // Update UI health
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if player is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Play jump sound
            if (jumpAudioSource != null && jumpAudioSource.clip != null)
            {
                jumpAudioSource.Play();
            }
        }

        // Handle invincibility timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            // Flashing effect
            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                // Reset color to full opacity
                Color resetColor = spriteRenderer.color;
                resetColor.a = 1f;
                spriteRenderer.color = resetColor;
            }
        }
    }

    void FixedUpdate()
    {
        // Move player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite based on movement direction
        if (moveInput > 0)
        {
            // Moving right - face right
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            // Moving left - face left  
            spriteRenderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag + " | Invincible: " + isInvincible);

        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play damage sound
        if (damageAudioSource != null && damageAudioSource.clip != null)
        {
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
            // Game Over
            GameManager.Instance.GameOver();
        }
        else
        {
            // Activate invincibility
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    // Visualize ground check in editor
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}