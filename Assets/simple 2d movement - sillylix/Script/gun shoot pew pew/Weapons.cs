using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public Animator playerAnimator;

    private SpriteRenderer spriteRenderer;

    private PlayerMovement2D playerController;

    public bool canFire = true;

    public float fireCooldown = 0.3f;
    private float fireTimer = 0f;
    private float gunOffsetX; // For gun position offset (for facing L and R)
    

    public int bullets = 30;
    public int maxBullets = 30;

    public Vector2 spawnOffset = new Vector2(0.8f, 0);
    

    private bool isFacingRight = true;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gunOffsetX = transform.localPosition.x;
        
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
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            FireGun();
        }

        // Handle facing direction based on player input
        if (Input.GetKeyDown(KeyCode.A))
        {
            isFacingRight = false;
            UpdateGunXPosition(); // Update the gun's X position when facing left
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isFacingRight = true;
            UpdateGunXPosition(); // Update the gun's X position when facing right
        }

        // Aim at cursor
        AimAtCursor();
    }

    private void FireGun()
    {
        if (bullets <= 0) return;
        bullets -= 1;

        // Trigger fire animation
        playerAnimator?.SetTrigger("FireRevolver");

        // Calculate the spawn position considering the gun's rotation
        Vector2 spawnPosition = (Vector2)transform.position + (isFacingRight ? spawnOffset : -spawnOffset);

        // Instantiate the bullet at the correct position
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        // Get direction to cursor and set bullet's direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        // Calculate direction to cursor
        Vector2 direction = (mousePosition - transform.position).normalized;
        newBullet.GetComponent<RevolverFunction>().SetDirection(direction);

        // Flip the bullet's sprite to match its direction
        newBullet.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

        canFire = false;
    }

    private void AimAtCursor()
    {
        // Get the cursor's position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        // Calculate the direction from the weapon to the cursor
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle of the weapon (Z-axis)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply Z-axis rotation normally (rotate to follow the cursor)
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, angle); // Keep X-axis locked, change only Y and Z

        // Flip the sprite based on the angle (Y-axis flip)
        if (angle > 90f || angle < -90f)
        {
            spriteRenderer.flipY = true;  // Flip the sprite along the Y-axis
        }
        else
        {
            spriteRenderer.flipY = false; // No flipping along the Y-axis
        }
    }

    public void UpdateGunXPosition()
    {
        // Adjust gun's X position based on player facing direction
        float xOffset = isFacingRight ? gunOffsetX : -gunOffsetX;
        transform.localPosition = new Vector2(xOffset, transform.localPosition.y);
    }
}