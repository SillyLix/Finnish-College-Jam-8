
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RevolverFunction : MonoBehaviour
{
    public bool hasHit = false;
    public float speed = 40f;
    public GameObject bulletHitbox;

    private Vector2 direction;
    private Rigidbody2D rb;

    // Initialize the bullet's Rigidbody2D (kinematic)
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // Ensure gravity is disabled for a kinematic Rigidbody2D
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normalize to keep it a unit vector

        // Set the bullet's rotation immediately based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle)); // Rotate the bullet
    }

    // Update is called once per frame
    void Update()
    {
        // If the bullet hasn't hit anything, move it
        if (!hasHit)
        {
            MoveBullet(); // Move the bullet manually
        }

        // If the bullet has hit something, destroy it
        if (hasHit)
        {
            Destroy(gameObject); // Destroy the bullet when it hits something
        }
    }

    // Jos osuu johonkin niin nuoli poistuu (EI TOIMI VIELL�)
    private void MoveBullet()
    {
        // Move the bullet manually using kinematic Rigidbody2D (direct position change)
        if (rb != null)
        {
            rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
        }
    }

    // Handle collisions with other objects (e.g., enemy or ground)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            bulletHitbox.SetActive(false);
            hasHit = true; // Mark the bullet as hit
        }
    }
}