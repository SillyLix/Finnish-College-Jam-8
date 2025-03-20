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
    private float gunOffsetX;

    public int bullets = 30;
    public int maxBullets = 30;
    public Vector2 spawnOffset = new Vector2(0.8f, 0);

    private bool isFacingRight = true;
    private float minAngleRight = -45f;
    private float maxAngleRight = 45f;
    private float minAngleLeft = 135f;
    private float maxAngleLeft = 225f;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunOffsetX = transform.localPosition.x;
    }

    void Update()
    {
        bullets = Mathf.Min(bullets, maxBullets);

        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                canFire = true;
                fireTimer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            FireGun();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            isFacingRight = false;
            UpdateGunXPosition();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isFacingRight = true;
            UpdateGunXPosition();
        }

        AimAtCursor();
    }

    private void FireGun()
    {
        if (bullets <= 0) return;
        bullets -= 1;
        playerAnimator?.SetTrigger("FireRevolver");

        Vector2 spawnPosition = (Vector2)transform.position + (isFacingRight ? spawnOffset : -spawnOffset);
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        Vector2 direction = (mousePosition - transform.position).normalized;
        newBullet.GetComponent<RevolverFunction>().SetDirection(direction);
        newBullet.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

        canFire = false;
    }

    private void AimAtCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (isFacingRight)
        {
            angle = Mathf.Clamp(angle, minAngleRight, maxAngleRight);
        }
        else
        {
            if (angle > 0)
                angle = Mathf.Clamp(angle, minAngleLeft - 360f, maxAngleLeft - 360f);
            else
                angle = Mathf.Clamp(angle, minAngleLeft, maxAngleLeft);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void UpdateGunXPosition()
    {
        float xOffset = isFacingRight ? gunOffsetX : -gunOffsetX;
        transform.localPosition = new Vector2(xOffset, transform.localPosition.y);
    }
}