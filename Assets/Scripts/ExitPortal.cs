using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private int requiredCrystals = 5;

    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Start with portal looking inactive (darker)
            Color portalColor = spriteRenderer.color;
            portalColor.a = 0.3f; // Make it semi-transparent at start
            spriteRenderer.color = portalColor;
        }
    }

    void Update()
    {
        // Always rotate portal
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Check if player has collected enough crystals
        if (GameManager.Instance != null && !isActive)
        {
            int crystalsCollected = GameManager.Instance.GetCrystalsCollected();

            if (crystalsCollected >= requiredCrystals)
            {
                ActivatePortal();
            }
        }
    }

    void ActivatePortal()
    {
        isActive = true;

        if (spriteRenderer != null)
        {
            // Make portal fully visible (active)
            Color portalColor = spriteRenderer.color;
            portalColor.a = 1f;
            spriteRenderer.color = portalColor;
        }

        Debug.Log("Portal is now active! Enter to win!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Portal collision with: " + other.gameObject.name + " | Active: " + isActive);

        if (other.CompareTag("Player"))
        {
            if (isActive)
            {
                // Player entered active portal - WIN!
                Debug.Log("Player entered active portal - triggering win!");
                GameManager.Instance.WinGame();
            }
            else
            {
                Debug.Log("Collect all crystals first! (" + GameManager.Instance.GetCrystalsCollected() + "/" + requiredCrystals + ")");
            }
        }
    }
}   