using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public Animator playerAnimator;

    private PlayerMovement2D playerController;

    public bool canFire = true;
    public float fireCooldown = 0.3f;
    private float fireTimer = 0f;

    public int bullets = 30;
    public int maxBullets = 30;

    public Vector2 spawnOffset = new Vector2(0.8f, 0);

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerController = FindObjectOfType<PlayerMovement2D>(); // Ensures we get the correct player controller

        // Get the SpriteRenderer component on the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Make sure bullet count doesn't exceed max
        if (bullets > maxBullets)
        {
            bullets = maxBullets;
        }

        // Handle firing
        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                canFire = true;
                fireTimer = 0f;
            }
        }

        // Fire the weapon when clicking and can fire
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            FireGun();
        }

        // Aim the gun at the cursor
        AimAtCursor();
    }

    private void FireGun()
    {
        if (bullets <= 0) return;
        bullets -= 1;

        // Trigger fire animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("FireRevolver");
        }

        // Determine where the bullet will spawn
        Vector2 spawnPosition = PlayerMovement2D.isFacingRight
            ? (Vector2)transform.position + spawnOffset
            : (Vector2)transform.position - spawnOffset;

        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        // Get direction to the cursor and set bullet's direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        Vector2 bulletDirection = (mousePosition - transform.position).normalized;
        RevolverFunction revolverScript = newBullet.GetComponent<RevolverFunction>();
        revolverScript.SetDirection(bulletDirection);

        // Flip the bullet based on direction
        if (bulletDirection.x < 0)
        {
            newBullet.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            newBullet.transform.localScale = new Vector3(1, 1, 1);
        }

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
}