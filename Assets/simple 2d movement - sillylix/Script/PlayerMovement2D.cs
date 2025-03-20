using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

// This script is for 2D movement with jump, dash, double jump, rigidbody movement, and transform movement
// You can use this script for 2D platformer games and 2D top-down games
// Made by Sillylix 2025 - https://www.sillylix.com/ - https://sillylix.itch.io/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

// MESSAGE FROM ANNE: I added in Flip(), HandleInputFlip() and called it in HandleMovement()
//not working tho :((
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
    private GunTransform gunTransform;
    private SpriteRenderer playerSpriteRenderer;

    #endregion

    void Start()
    {
        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = gravityScale;

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer == null)
        {
            Debug.LogError("Player SpriteRenderer not found! Make sure the player has a SpriteRenderer attached.");
        }

        gunTransform = GetComponentInChildren<GunTransform>();
        if (gunTransform == null)
        {
            Debug.LogError("GunTransform script not found! Make sure the player has a child object with a weapon.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleDashing();
        HandlePlayerSpriteFlip();
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
        }

        if (verticalMovementNeeded)
        {
            rb2d.gravityScale = 0; // Disable gravity for top-down movement
            verticalInput = Input.GetAxis("Vertical");
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, verticalInput * playerSpeed);
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
            }
            else if (canDoubleJump && Input.GetKeyDown(jumpKey))
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
                canDoubleJump = false;
            }

            // Increase gravity when pressing down (fast fall)
            if (Input.GetKey(KeyCode.DownArrow) && !isGrounded)
            {
                rb2d.gravityScale = gravityScale * 2;
            }
            else
            {
                rb2d.gravityScale = gravityScale;
            }
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
    void HandlePlayerSpriteFlip()
    {
        if (gunTransform != null)
        {
            // Flip player sprite based on the gun's Y-axis flip
            if (gunTransform.IsGunSpriteFlipped())
            {
                playerSpriteRenderer.flipX = true; // Flip player sprite to face left
            }
            else
            {
                playerSpriteRenderer.flipX = false; // Flip player sprite to face right
            }
        }
    }

}