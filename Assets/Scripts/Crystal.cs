using UnityEngine;

/// <summary>
/// Handles crystal collectible logic including visual effects and scoring.
/// When the player collects a crystal, it adds points and plays audio.
/// </summary>
public class Crystal : MonoBehaviour
{
    /// <summary>
    /// The number of points awarded to the player when this crystal is collected.
    /// </summary>
    [SerializeField] private int pointValue = 10;

    /// <summary>
    /// How fast the crystal rotates on the Z-axis (degrees per second).
    /// </summary>
    [SerializeField] private float rotationSpeed = 100f;

    /// <summary>
    /// How fast the crystal pulses (grows and shrinks) in cycles per second.
    /// </summary>
    [SerializeField] private float pulseSpeed = 2f;

    /// <summary>
    /// The magnitude of the pulsing effect (0.1 = 10% size variation).
    /// </summary>
    [SerializeField] private float pulseAmount = 0.1f;

    /// <summary>
    /// Stores the original scale of the crystal for the pulse effect.
    /// </summary>
    private Vector3 originalScale;

    /// <summary>
    /// Audio source component for playing the collection sound.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Initialize crystal components and save the original scale.
    /// </summary>
    void Start()
    {
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Continuously animate the crystal by rotating and pulsing it.
    /// </summary>
    void Update()
    {
        // Rotate crystal continuously for visual appeal
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Apply a pulsing effect (grows and shrinks using sine wave)
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;
    }

    /// <summary>
    /// Called when the player enters the crystal's collision zone.
    /// Collects the crystal, adds points, and plays audio.
    /// </summary>
    /// <param name="other">The collider that entered this trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collected this crystal
        if (other.CompareTag("Player"))
        {
            // Add points to the player's score via GameManager
            GameManager.Instance.AddScore(pointValue);

            // Hide the crystal immediately to give visual feedback
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Play the collection sound effect (if SFX is not muted)
            if (audioSource != null && audioSource.clip != null)
            {
                if (AudioManager.Instance == null || !AudioManager.Instance.isSfxMuted)
                    audioSource.Play();
            }

            // Destroy the crystal object after the sound finishes playing (0.5 second delay)
            Destroy(gameObject, 0.5f);
        }
    }
}