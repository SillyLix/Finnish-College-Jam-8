using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject bullet;
    public Animator playerAnimator;

    private bool canFire = true;
    private float fireCooldown = 0.1f;
    private float fireTimer = 0f;

    [SerializeField] private int bullets = 30;
    [SerializeField] private int maxBullets = 100;  // Set a default max bullet capacity
    [SerializeField] private AudioClip soundname;


    private GunTransform gunTransform;

    void Start()
    {
        gunTransform = GetComponentInParent<GunTransform>();  // Assuming the GunTransform is on the parent (gun)
        maxBullets = bullets;
    }

    void Update()
    {
        // Clamp bullet count within max bullets
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

        if (Input.GetMouseButton(0) && canFire && bullets > 0)
        {
            FireGun();
        }

        // Adjust the barrel Y position based on gun flip (for proper positioning)
        AdjustBarrelPosition();
    }

    private void FireGun()
    {
        if (bullets <= 0) return;

        bullets -= 1;

        playerAnimator?.SetTrigger("FireRevolver");
        SoundFXManager.instance.PlaySoundFXClip(soundname, transform, 1f);


        // Use the barrel's position for spawning bullets
        Vector2 spawnPosition = transform.position;  // Since this script is on the barrel, use its position
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        // Get direction to cursor and set bullet's direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));

        Vector2 direction = (mousePosition - transform.position).normalized;
        RevolverFunction bulletScript = newBullet.GetComponent<RevolverFunction>();
        bulletScript.SetDirection(direction);

        canFire = false;
    }

    // Adjust the barrel Y position depending on whether the gun is flipped
    private void AdjustBarrelPosition()
    {
        // No need to reference barrel explicitly since the script is on the barrel
        if (gunTransform.isFacingRight)
        {
            // Normal position when facing right
            transform.localPosition = new Vector3(transform.localPosition.x, 0.035f, transform.localPosition.z);
        }
        else
        {
            // Adjust position when facing left (flipped)
            transform.localPosition = new Vector3(transform.localPosition.x, -0.03f, transform.localPosition.z);
        }
    }
}