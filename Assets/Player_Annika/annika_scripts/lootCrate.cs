using UnityEngine;

public class lootCrate : MonoBehaviour
{
    public HealthSystem healthSystemScript;

    private float crateHealth = 3f;
    private float crateDamage = 1f;

    public GameObject playersBullet;
    private Collider2D Collider2D;
    public Animator animator;

    private void Start()
    {
        healthSystemScript = GetComponent<HealthSystem>();
        Collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (crateHealth > 0)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                animator.Play("Crate_damage");
                crateHealth -= crateDamage;
                Destroy(collision.gameObject);
            }
        }

        else if (crateHealth <= 0)
        {
            healthSystemScript.Heal(20);
            animator.Play("Crate_breaking");
            Destroy(gameObject);
        }

    }

}
