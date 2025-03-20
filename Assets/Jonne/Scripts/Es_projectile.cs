using UnityEngine;

public class Es_projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 100f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after a set time
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Handle player damage here
            Debug.Log("Player hit by projectile!");
            Destroy(gameObject); // Destroy the projectile on hit
        }
    }
}