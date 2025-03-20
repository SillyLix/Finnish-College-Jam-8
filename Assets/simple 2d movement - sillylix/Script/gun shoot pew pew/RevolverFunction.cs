using UnityEngine;

public class RevolverFunction : MonoBehaviour
{
    public bool hasHit = false;
    public float speed = 40f;
    public GameObject bulletHitbox;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // Ensure gravity is disabled for a kinematic Rigidbody2D
        }

        // Destroy the bullet after 5 seconds
        Destroy(gameObject, 5f);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    void Update()
    {
        if (!hasHit)
        {
            MoveBullet();
        }

        if (hasHit)
        {
            Destroy(gameObject);
        }
    }

    private void MoveBullet()
    {
        if (rb != null)
        {
            rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
        }
    }
}