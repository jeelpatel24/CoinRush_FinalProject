using UnityEngine;

/// <summary>
/// Controls enemy patrol behavior.
/// Enemies move back and forth within a specified distance range.
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// How fast the enemy moves horizontally (units per second).
    /// </summary>
    [SerializeField] private float moveSpeed = 2f;

    /// <summary>
    /// Maximum distance the enemy can travel from its starting position.
    /// The enemy turns around when it reaches this distance.
    /// </summary>
    [SerializeField] private float moveDistance = 3f;

    /// <summary>
    /// The starting position of the enemy used as a reference point.
    /// </summary>
    private Vector3 startPosition;

    /// <summary>
    /// Direction multiplier: 1 for right, -1 for left.
    /// </summary>
    private int direction = 1;

    /// <summary>
    /// Initialize the starting position when the game starts.
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// Update enemy position each frame and handle direction reversal.
    /// </summary>
    void Update()
    {
        // Move enemy left or right depending on current direction
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Calculate how far the enemy has moved from the starting position
        float distanceMoved = transform.position.x - startPosition.x;

        // If enemy reached the distance limit, reverse direction
        if (Mathf.Abs(distanceMoved) >= moveDistance)
        {
            direction *= -1; // Flip direction: 1 → -1 or -1 → 1
        }
    }
}