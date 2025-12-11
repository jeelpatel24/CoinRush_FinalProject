using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int pointValue = 10;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmount = 0.1f;

    private Vector3 originalScale;
    private AudioSource audioSource;

    void Start()
    {
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotate crystal for visual appeal
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Pulse effect (grow and shrink)
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to game manager
            GameManager.Instance.AddScore(pointValue);

            // Hide crystal immediately (disable renderer and collider)
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Play collection sound
           // Play collection sound
            if (audioSource != null && audioSource.clip != null)
            {
              if (AudioManager.Instance == null || !AudioManager.Instance.isSfxMuted)
              audioSource.Play();
            }

            // Destroy crystal after sound plays (0.5 second delay)
            Destroy(gameObject, 0.5f);
        }
    }
}