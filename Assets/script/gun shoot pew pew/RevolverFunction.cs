using UnityEngine;

public class RevolverFunction : MonoBehaviour
{
    public bool hasHit = false;
    public float speed = 40f;
    public GameObject bulletHitBox;

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

        // Ensure the bullet is facing the correct direction as soon as it's instantiated
        RotateBullet();
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normalize to keep it a unit vector
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

    private void MoveBullet()
    {
        // Move the bullet manually using kinematic Rigidbody2D (direct position change)
        if (rb != null)
        {
            Vector2 newPosition = (Vector2)transform.position + (direction * (speed * Time.deltaTime));
            rb.MovePosition(newPosition);
        }
    }

    public void RotateBullet()
    {
        // Calculate the angle in degrees of the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the bullet to match the direction (on the Z-axis)
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    // Handle collisions with other objects (e.g., enemy or ground)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            bulletHitBox.SetActive(false);
            hasHit = true; // Mark the bullet as hit
            Destroy(gameObject); // Destroy the bullet
        }
    }
}