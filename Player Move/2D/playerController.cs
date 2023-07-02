using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float maxJumpTime = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = 0.3f;

    private bool isJumping;
    private float jumpTime;
    private bool isGrounded;
    private bool canCoyoteJump;
    private bool isTouchingWall;
    private bool isWallSliding;
    private Rigidbody2D rb;
    private Collider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || canCoyoteJump)
            {
                isJumping = true;
                canCoyoteJump = false;
                jumpTime = Time.time + maxJumpTime;
            }
        }

        // Check for release of jump button
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (isTouchingWall && !isGrounded)
        {
            if ((moveX > 0 && isWallSliding) || (moveX < 0 && !isWallSliding))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }
        }

        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // Jumping
        if (isJumping && Time.time < jumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Check if the player is grounded
        isGrounded = IsGrounded();
        isTouchingWall = IsTouchingWall();

        // Enable coyote time (short delay after leaving a platform to still allow jumping)
        if (isGrounded)
        {
            canCoyoteJump = true;
            isWallSliding = false;
            Debug.Log("Coyote Jump enabled");
        }
        else if (!isGrounded && canCoyoteJump && Time.time > jumpTime)
        {
            canCoyoteJump = false;
            Debug.Log("Coyote Jump disabled");
        }

        // Wall sliding
        if (!isGrounded && isTouchingWall && rb.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);
        return hit.collider != null;
    }

    private bool IsTouchingWall()
    {
        float extraWidth = 0.01f;
        RaycastHit2D hitRight = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, extraWidth, wallLayer);
        if (hitRight.collider != null)
            return true;

        RaycastHit2D hitLeft = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, extraWidth, wallLayer);
        return hitLeft.collider != null;
    }
}