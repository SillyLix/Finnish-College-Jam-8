using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RevolverFunction : MonoBehaviour
{
    public bool hasHit = false;
    public float speed = 40f;
    public GameObject bulletHitbox;

    public static bool facing;
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normalize to keep it a unit vector
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement2D controller = GetComponent<PlayerMovement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Miten panos liikkuu(tarvitsee muutoksia suunnan vaihdon takia!)
        if (!hasHit)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
        if (hasHit)
        {
            Destroy(gameObject);
        }
    }

    // Jos osuu johonkin niin nuoli poistuu (EI TOIMI VIELL�)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        //if (collision.gameObject.CompareTag("Bomb"))
        //{
        //    Destroy(gameObject);
        //}
        //if (collision.gameObject.CompareTag("Golem"))
        //{
        //    Destroy(gameObject);
        //}
        //if (collision.gameObject.CompareTag("Dialogue"))
        //{
        //    return;
        //}
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    bulletHitbox.SetActive(false);
        //    hasHit = true;
        //}
        else
        {
            return;
        }

    }

}