using UnityEngine;

public class GunTransform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float gunOffsetX; // For gun position offset (for facing L and R)

    private bool isFacingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunOffsetX = transform.localPosition.x;
    }

    void Update()
    {
        // Aim at cursor
        AimAtCursor();
    }

    public void UpdateGunXPosition()
    {
        // Adjust gun's X position based on player facing direction
        float xOffset = isFacingRight ? gunOffsetX : -gunOffsetX;
        transform.localPosition = new Vector2(xOffset, transform.localPosition.y);
    }

    private void FlipSprite()
    {
        // Flip the gun sprite using the sprite renderer
        spriteRenderer.flipX = !spriteRenderer.flipX;
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
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the sprite based on the angle (Y-axis flip)
        if (angle > 90f || angle < -90f)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }

    // Return whether the gun's sprite is flipped along the Y-axis
    public bool IsGunSpriteFlipped()
    {
        return spriteRenderer.flipY;
    }
}