using UnityEngine;

public class GunTransform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    private float gunOffsetX; // For gun position offset (for facing L and R)

    public bool isFacingRight = true;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunOffsetX = transform.localPosition.x;
    }

    void Update()
    {
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

    public void UpdateGunXPosition()
    {
        // Adjust gun's X position based on player facing direction
        float xOffset = isFacingRight ? gunOffsetX : -gunOffsetX;
        transform.localPosition = new Vector2(xOffset, transform.localPosition.y);
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