using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move enemy back and forth
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Check if reached move distance limit
        float distanceMoved = transform.position.x - startPosition.x;

        if (Mathf.Abs(distanceMoved) >= moveDistance)
        {
            direction *= -1; // Reverse direction
        }
    }
}