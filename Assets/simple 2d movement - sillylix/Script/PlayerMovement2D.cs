using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

// This script is for 2D movement with jump, dash, double jump, rigidbody movement, and transform movement
// You can use this script for 2D platformer games and 2D top-down games
// Made by Sillylix 2025 - https://www.sillylix.com/ - https://sillylix.itch.io/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]


public class PlayerMovement2D : MonoBehaviour
{
    #region Variables

    // --- Player Movement Settings ---
    [SerializeField] private bool horizontalMovementNeeded = true;
    [SerializeField] public bool verticalMovementNeeded = false;
    [SerializeField] private float playerSpeed = 5f;

    // --- Jump Settings ---
    public bool jumpNeeded = false;
    public bool doubleJumpNeeded = false;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float groundDetectorLength = 1f;
    public LayerMask groundLayer;
    [SerializeField] private bool drawJumpDetector = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    // --- Dash Settings ---
    [Tooltip("Enable this if you want the player to dash.")]
    public bool dashEnabled = false;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    // --- Private Variables ---
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;
    private bool isTouchingWall = false;
    private Rigidbody2D rb2d;

    //ANNES ADDED VARIABLES
    public static bool isFacingRight = true;
    private Weapons weaponScript;

    // ANNIKA'S ADDED VARIABLES
    public Animator animator;

    #endregion

    void Start()
    {
        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = gravityScale;

        animator = GetComponent<Animator>();

        weaponScript = GetComponentInChildren<Weapons>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleDashing();
        HandleInputFlip();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    void HandleMovement()
    {
        if (horizontalMovementNeeded && !isTouchingWall)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rb2d.linearVelocity = new Vector2(horizontalInput * playerSpeed, rb2d.linearVelocity.y);

            // Set animation based on movement
            animator.SetBool("isWalking", horizontalInput != 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void HandleJumping()
    {
        if (jumpNeeded)
        {
            if ((isGrounded || isTouchingWall) && Input.GetKeyDown(jumpKey))
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
                canDoubleJump = doubleJumpNeeded;
                animator.SetBool("isJumping", true);
            }
            else if (canDoubleJump && Input.GetKeyDown(jumpKey))
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
                canDoubleJump = false;
                animator.SetBool("isJumping", true);
            }
        }

        // Reset jump animation when grounded
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void HandleDashing()
    {
        if (dashEnabled && Input.GetKeyDown(dashKey) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void CheckGrounded()
    {
        // Raycast for detecting the ground
        Vector2 rayOrigin = transform.position + Vector3.down * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundDetectorLength, groundLayer);

        // Draw the jump detector ray in the editor
        if (drawJumpDetector)
        {
            Debug.DrawRay(rayOrigin, Vector2.down * groundDetectorLength, Color.red);
        }

        isGrounded = hit.collider != null;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float originalSpeed = playerSpeed;
        playerSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        playerSpeed = originalSpeed;
        isDashing = false;
    }

    // Check if the player is touching the sides of a wall
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (Mathf.Abs(normal.x) > 0.5f) // If the normal is mostly horizontal
            {
                isTouchingWall = true;
            }
        }
    }

    // Reset the wall collision flag when exiting a wall
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isTouchingWall = false;
        }
    }

    IEnumerator waitBeforTurningOffJump()
    {
        yield return new WaitForFixedUpdate();
    }
    void HandleInputFlip()
    {
        // Check for A or D key press and flip accordingly
        if (Input.GetKeyDown(KeyCode.A) && isFacingRight) // Pressing A, should flip to the left
        {
            Flip();
            weaponScript.UpdateGunXPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isFacingRight) // Pressing D, should flip to the right
        {
            Flip();
            weaponScript.UpdateGunXPosition();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the sprite by changing the sign of the x scale
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;
    }
}