using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public Animator playerAnimator;

    //private ScoreHP scoreHP;
    private PlayerMovement2D playerController;

    public bool canFire = true;
    public float fireCooldown = 0.3f;
    private float fireTimer = 0f;

    public int bullets = 30;
    public int maxBullets = 30;

    public Vector2 spawnOffset = new Vector2(0.8f, 0);

    void Start()
    {
        //scoreHP = FindObjectOfType<ScoreHP>();
        playerController = Object.FindFirstObjectByType<PlayerMovement2D>();
    }

    void Update()
    {
        //if (!PauseMenu.isGamePaused)
        //{

        //}

        if (bullets > maxBullets)
        {
            bullets = maxBullets;
        }

        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                canFire = true;
                fireTimer = 0f;
            }
        }

        // Fire gun if possible
        if (Input.GetKeyDown(KeyCode.E) && canFire)
        {
            Gun();
            //scoreHP.UpdatePowerUpSlot();
        }
    }

    public void Gun()
    {
        if (bullets <= 0) return;
        bullets -= 1;

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("FireRevolver");
        }

        Vector2 spawnPosition = PlayerMovement2D.isFacingRight
            ? (Vector2)transform.position + spawnOffset
            : (Vector2)transform.position - spawnOffset;

        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);

        //if (!RevolverFunction.facing)
        //{
        //    newBullet.transform.localScale = new Vector3(-1, 1, 1);
        //}
        //else
        //{
        //    newBullet.transform.localScale = new Vector3(1, 1, 1);
        //}


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = transform.position.z;

        Vector2 bulletDirection = (mousePosition - transform.position).normalized;

        RevolverFunction revolverScript = newBullet.GetComponent<RevolverFunction>();
        //Vector2 bulletDirection = PlayerMovement2D.isFacingRight ? Vector2.right : Vector2.left;
        revolverScript.SetDirection(bulletDirection);

        canFire = false;
    }
}