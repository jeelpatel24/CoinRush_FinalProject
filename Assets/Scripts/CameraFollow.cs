using UnityEngine;

/// <summary>
/// Controls the main camera to smoothly follow the player character.
/// Uses lerping for smooth movement instead of rigid following.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Reference to the player's Transform component.
    /// The camera will follow this position.
    /// </summary>
    [SerializeField] private Transform player;

    /// <summary>
    /// How fast the camera smoothly transitions to the desired position.
    /// Lower values = smoother, slower movement (0 = no movement, 1 = instant).
    /// </summary>
    [SerializeField] private float smoothSpeed = 0.125f;

    /// <summary>
    /// Offset from the player's position where the camera should be.
    /// (0, 1, -10) positions camera slightly above and behind the player.
    /// </summary>
    [SerializeField] private Vector3 offset = new Vector3(0, 1, -10);

    /// <summary>
    /// Called after all Update() calls each frame.
    /// Smoothly moves the camera to follow the player's position.
    /// </summary>
    void LateUpdate()
    {
        // Only move camera if player reference exists
        if (player != null)
        {
            // Calculate where the camera should be (player position + offset)
            Vector3 desiredPosition = player.position + offset;

            // Smoothly interpolate from current position to desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;
        }
    }
}