using UnityEngine;

/// <summary>
/// Manages the exit portal that allows the player to complete the level.
/// The portal is inactive until the player collects all required crystals.
/// When active, the portal becomes visible and can trigger a level win.
/// </summary>
public class ExitPortal : MonoBehaviour
{
    // ------------------------------
    // Portal Settings
    // ------------------------------
    
    /// <summary>
    /// How fast the portal rotates on the Z-axis (degrees per second).
    /// </summary>
    [SerializeField] private float rotationSpeed = 50f;

    /// <summary>
    /// The number of crystals the player must collect to activate the portal.
    /// </summary>
    [SerializeField] private int requiredCrystals = 5;

    /// <summary>
    /// Indicates whether the portal is active and can be used to win the level.
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// The SpriteRenderer component used to control the portal's visibility.
    /// </summary>
    private SpriteRenderer spriteRenderer;

    // ------------------------------
    // Initialization
    // ------------------------------
    /// <summary>
    /// Called on the first frame when the script is enabled.
    /// Initializes the portal in an inactive state with reduced opacity.
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Start with portal semi-transparent to show it's inactive
            Color portalColor = spriteRenderer.color;
            portalColor.a = 0.3f;   // Faded appearance when inactive
            spriteRenderer.color = portalColor;
        }
    }

    // ------------------------------
    // Update - Rotate portal and check activation
    // ------------------------------
    /// <summary>
    /// Called once per frame.
    /// Rotates the portal continuously and checks if it should be activated.
    /// </summary>
    void Update()
    {
        // Rotate portal continuously for visual effect
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Check if player collected enough crystals to activate the portal
        if (GameManager.Instance != null && !isActive)
        {
            int crystalsCollected = GameManager.Instance.GetCrystalsCollected();

            // When player collects all required crystals → activate portal
            if (crystalsCollected >= requiredCrystals)
            {
                ActivatePortal();
            }
        }
    }

    // ------------------------------
    // Activate portal (player can now win)
    // ------------------------------
    /// <summary>
    /// Called when the player collects all required crystals.
    /// Makes the portal fully visible and ready to be used.
    /// </summary>
    void ActivatePortal()
    {
        isActive = true;

        // Make portal fully visible to indicate it's now active and usable
        if (spriteRenderer != null)
        {
            Color portalColor = spriteRenderer.color;
            portalColor.a = 1f;    // Fully opaque when active
            spriteRenderer.color = portalColor;
        }

        Debug.Log("Portal is now active! Player can enter to win!");
    }

    // ------------------------------
    // Trigger Detection (Player touches portal)
    // ------------------------------
    /// <summary>
    /// Called when another collider enters this trigger zone.
    /// Handles player collision with the portal.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Portal collision with: " + other.gameObject.name + " | Active: " + isActive);

        // Only respond to the player collider
        if (other.CompareTag("Player"))
        {
            if (isActive)
            {
                // Player entered an active portal → trigger level win
                Debug.Log("Player entered active portal - triggering win!");
                GameManager.Instance.WinGame();
            }
            else
            {
                // Portal is not active yet — remind player to collect crystals first
                Debug.Log("Collect all crystals first! (" +
                    GameManager.Instance.GetCrystalsCollected() + "/" + requiredCrystals + ")");
            }
        }
    }
}
