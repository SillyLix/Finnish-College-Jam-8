using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject bullet;
    public Animator playerAnimator;

    private bool canFire = true;
    private float fireCooldown = 0.3f;
    private float fireTimer = 0f;
    [SerializeField] private int bullets = 30;
    int maxBullets;

    public Vector2 spawnOffset = new Vector2(0.8f, 0);  // Offset to position the bullet spawn point

    private GunTransform gunTransform; // Reference to the GunTransform script

    void Start()
    {
        gunTransform = GetComponentInParent<GunTransform>(); // Get the GunTransform script attached to the gun's parent
        maxBullets = bullets;
    }

    void Update()
    {
        // Clamp bullet count within max bullets
        bullets = Mathf.Min(bullets, maxBullets);

        // Handle firing cooldown
        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                canFire = true;
                fireTimer = 0f;
            }
        }

        // Fire weapon
        if (Input.GetMouseButton(0) && canFire)
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        if (bullets <= 0) return;
        bullets -= 1;

        // Trigger fire animation
        playerAnimator?.SetTrigger("FireRevolver");

        // Spawn the bullet at the empty GameObject's position (which is positioned at the barrel)
        Vector2 spawnPosition = transform.position;  // Use the position of the empty GameObject (parented to the gun)

        // Instantiate the bullet at the spawn position
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        // Get direction to cursor and set bullet's direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        // Calculate direction to cursor
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Set the bullet's direction in RevolverFunction (movement and rotation will be handled there)
        RevolverFunction bulletScript = newBullet.GetComponent<RevolverFunction>();
        bulletScript.SetDirection(direction);  // Set the direction of the bullet in RevolverFunction

        // Make sure the bullet is facing the correct way before moving
        bulletScript.RotateBullet();

        canFire = false;
    }
}