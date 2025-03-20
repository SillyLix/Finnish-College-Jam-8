using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    #region Variables
    private AIPath aIPath;

    [Required, SerializeField]
    private GameObject player;

    [SerializeField, BoxGroup("Enemy Info"), Required]
    private int speed = 2;

    [SerializeField, BoxGroup("Enemy Info"), Required]
    private int damage = 10;


    [SerializeField, BoxGroup("Enemy Info"), Required]
    private float attackRange = 1f;

    [BoxGroup("Enemy type")]
    [SerializeField, HorizontalGroup("Enemy type/type")]
    private bool shooter;

    [SerializeField, BoxGroup("Enemy Info"), Required, ShowIf("shooter")]
    private GameObject bulletSpawnPoint;
    [SerializeField, BoxGroup("Enemy Info"), Required, ShowIf("shooter")]
    private GameObject bulletPrefab;

    [BoxGroup("Enemy type")]
    [SerializeField, HorizontalGroup("Enemy type/type")]
    private bool touch = true;
    [BoxGroup("Enemy type")]
    [SerializeField, HorizontalGroup("Enemy type/type")]
    private bool boomer;
    private float lastAttackTime = 0f;

    [SerializeField, BoxGroup("Enemy Info"), Required, ShowIf("boomer")]
    private int boomDmg = 20;

    private float currentDistance;

    private HealthSystem playerHealthSystem;
    private HealthSystem enemyHealthSystem;
    #endregion

    void Start()
    {
        transform.LookAt(player.transform.position);
        transform.Rotate(0, -90, 0); // Adjust the rotation to face the player correctly
        aIPath = GetComponent<AIPath>();
        playerHealthSystem = player.GetComponent<HealthSystem>();
        enemyHealthSystem = GetComponent<HealthSystem>();

        // Ensure only one enemy type is selected
        int selectedTypes = 0;
        if (shooter) selectedTypes++;
        if (touch) selectedTypes++;
        if (boomer) selectedTypes++;

        if (selectedTypes != 1)
        {
            Debug.LogError("Exactly one enemy type must be selected.");
        }
    }

    void Update()
    {
        if (aIPath == null)
        {
            Debug.LogErrorFormat("AIPath component not found on {0}", gameObject.name);
            return;
        }

        aIPath.maxSpeed = speed;
        aIPath.destination = player.transform.position;
        aIPath.endReachedDistance = attackRange;
        currentDistance = Vector2.Distance(transform.position, player.transform.position);

        // Flip the sprite based on the direction of movement
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check if the player is within attack range
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if (shooter)
            {
                if (Time.time - lastAttackTime >= 1f)
                {
                    Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, gameObject.GetComponentInChildren<Transform>().rotation);
                    lastAttackTime = Time.time;
                }
            }
            else if (touch)
            {
                if (Time.time - lastAttackTime >= 1f)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
            else if (boomer)
            {
                if (enemyHealthSystem.CurrentHealth - damage <= 0)
                {
                    playerHealthSystem.TakeDamage(boomDmg);
                    enemyHealthSystem.TakeDamage(boomDmg);
                }
                else if (Time.time - lastAttackTime >= 1f)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
        }
        void AttackPlayer()
        {
            playerHealthSystem.TakeDamage(damage);
        }
    }
}
