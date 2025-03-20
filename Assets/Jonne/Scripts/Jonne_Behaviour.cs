using UnityEngine;

public class Jonne_Behaviour : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float runSpeed = 6f; // Speed when running
    public float walkRange = 7f; // Distance to start walking
    public float runRange = 9f; // Distance to start running
    public float shootRange = 5f; // Distance to start shooting

    [Header("Projectile")]
    public GameObject ES_0; // Projectile prefab
    public Transform TolkkiSpawnPoint; // Spawn point for projectiles
    public float projectileCooldown = 2f; // Cooldown between projectiles

    private Transform player;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isClose;
    private bool shooting;
    private float lastProjectileTime;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Get the Rigidbody2D component
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D not found on Jonne! Add a Rigidbody2D component.");
        }

        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on Jonne! Add an Animator component.");
        }

        // Allow immediate attack
        lastProjectileTime = -projectileCooldown;
    }

    void Update()
    {
        if (player == null) return; // Skip if player is not found

        // Calculate direction and distance to the player
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (!shooting)
        {
            if (distance > walkRange) // Player is far away
            {
                isClose = false;

                if (distance > runRange) // Player is very far, run
                {
                    Run();
                }
                else // Player is moderately far, walk
                {
                    Walk();
                }
            }
            else // Player is close
            {
                isClose = true;

                // Stop moving and prepare to shoot
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("idle", true);

                shooting = true;
                StartCoroutine(Shoot()); // Start shooting
            }
        }
    }

    void Walk()
    {
        // Move towards the player at walking speed
        Vector2 direction = (player.position - transform.position).normalized;
        rigidbody2D.velocity = direction * moveSpeed;

        // Update animator
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("idle", false);

        Debug.Log("Walking towards player");
    }

    void Run()
    {
        // Move towards the player at running speed
        Vector2 direction = (player.position - transform.position).normalized;
        rigidbody2D.velocity = direction * runSpeed;

        // Update animator
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("idle", false);

        Debug.Log("Running towards player");
    }

    System.Collections.IEnumerator Shoot()
    {
        while (isClose)
        {
            if (Time.time >= lastProjectileTime + projectileCooldown)
            {
                ThrowProjectile();
                lastProjectileTime = Time.time;
            }

            yield return null; // Wait for the next frame
        }

        // Stop shooting when the player is no longer close
        shooting = false;
    }

    void ThrowProjectile()
    {
        if (ES_0 == null)
        {
            Debug.LogError("Projectile prefab (ES_0) is not assigned!");
            return;
        }

        if (TolkkiSpawnPoint == null)
        {
            Debug.LogError("Spawn point (TolkkiSpawnPoint) is not assigned!");
            return;
        }

        // Spawn the projectile at the spawn point
        Instantiate(ES_0, TolkkiSpawnPoint.position, TolkkiSpawnPoint.rotation);
        Debug.Log("Projectile thrown from position: " + TolkkiSpawnPoint.position);
    }
}