using System.Collections;
using UnityEngine;

public class kitten : MonoBehaviour
{
    public GameObject bulletPrefab;  // Luoti-prefab
    public Transform bulletSpawnPoint; // Luodin l�ht�piste
    public float bulletSpeed = 10f;  // Luodin nopeus
    public float detectionRange = 5f; // Et�isyys, jolla vihollinen alkaa ampua
    public float shootInterval = 3f; // Aikav�li laukausten v�lill�

    private Transform player;
    private Animator anim;
    private bool canShoot = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Etsii pelaajan tagin perusteella
        anim = GetComponent<Animator>(); // Hakee animaattorin
        StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectionRange)
            {
                canShoot = true;
                anim.SetBool("isShooting", true);
            }
            else
            {
                canShoot = false;
                anim.SetBool("isShooting", false);
                Debug.Log("Player out of range");
            }
        }
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (canShoot)
            {
                Shoot();
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed; // Liikkuu vihollisen eteenp�in suuntaan
    }
}