using UnityEngine;

public class Jonne_Behaviour : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f; // Range to detect the player
    public float retreatRange = 2f; // Range to retreat from the player
    public float shootRange = 4f; // Range to shoot projectiles

    [Header("Projectile")]
    public GameObject ES_0; // Projectile prefab
    public Transform TölkkiSpawnPoint; // Spawn point for projectiles
    public float projectileCooldown = 2f; // Cooldown between projectiles

    private Transform player;
    private Rigidbody2D rigidbody2D;
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

        // Allow immediate attack
        lastProjectileTime = -projectileCooldown;
    }

    void Update()
    {
        if (player == null) return; // Skip if player is not found

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer <= detectionRange)
        {
            MoveBoss(distanceToPlayer);
            HandleAttack(distanceToPlayer);
        }
        else
        {
            // Stop moving if the player is out of range
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    void MoveBoss(float distanceToPlayer)
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (distanceToPlayer > retreatRange)
        {
            // Move towards the player
            rigidbody2D.velocity = direction * moveSpeed;
            Debug.Log("Moving towards player");
        }
        else if (distanceToPlayer < retreatRange)
        {
            // Move away from the player
            rigidbody2D.velocity = -direction * moveSpeed;
            Debug.Log("Moving away from player");
        }
    }

    void HandleAttack(float distanceToPlayer)
    {
        if (distanceToPlayer <= shootRange && Time.time >= lastProjectileTime + projectileCooldown)
        {
            ThrowProjectile();
            lastProjectileTime = Time.time; // Reset cooldown
        }
    }

    void ThrowProjectile()
    {
        if (ES_0 == null)
        {
            Debug.LogError("Projectile prefab (ES_0) is not assigned!");
            return;
        }

        if (TölkkiSpawnPoint == null)
        {
            Debug.LogError("Spawn point (TölkkiSpawnPoint) is not assigned!");
            return;
        }

        // Spawn the projectile at the spawn point
        Instantiate(ES_0, TölkkiSpawnPoint.position, TölkkiSpawnPoint.rotation);
        Debug.Log("Projectile thrown from position: " + TölkkiSpawnPoint.position);
    }
}