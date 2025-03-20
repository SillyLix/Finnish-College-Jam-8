using UnityEngine;

public class bullet : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 10f; // Speed of the bullet
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Disable gravity for the bullet
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.linearVelocity = transform.forward * speed; // Move the bullet forward at a speed of 10 units per second
    }

}
