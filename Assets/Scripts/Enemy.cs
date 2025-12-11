using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;      // How fast the enemy moves
    [SerializeField] private float moveDistance = 3f;   // How far the enemy moves from its start point

    private Vector3 startPosition;  // Initial spawn position
    private int direction = 1;      // 1 = right, -1 = left

    void Start()
    {
        // Store the starting position so we know how far the enemy has moved
        startPosition = transform.position;
    }

    void Update()
    {
        // Move enemy horizontally based on direction (+1 or -1)
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Distance moved from start
        float distanceMoved = transform.position.x - startPosition.x;

        // If the enemy moved past the allowed distance, flip direction
        if (Mathf.Abs(distanceMoved) >= moveDistance)
        {
            direction *= -1; // Reverse movement
        }
    }
}
