using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float maxJumpTime = 0.3f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool isJumping;
    private float jumpTime;
    private bool isGrounded;
    private bool isTouchingWallleft;
    private bool isTouchingWallright;
    private Rigidbody2D rb;
    // private Collider2D coll;
    private CapsuleCollider2D coll;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        // coll = GetComponent<Collider2D>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update(){
        // Coyote time
        if (isGrounded){
            coyoteTimer = Time.time + coyoteTime;
        }

        // Jump buffer time
        if (jumpBufferTimer > 0){
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer >= 0 && (isGrounded || Time.time <= coyoteTimer))
            {
                isJumping = true;
                jumpTime = Time.time + maxJumpTime;
                jumpBufferTimer = 0;
                Debug.Log("Buffered jump");
            }
        }

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isGrounded || Time.time <= coyoteTimer)
            {
                isJumping = true;
                jumpTime = Time.time + maxJumpTime;
                Debug.Log("Jumping");
            }
            else
            {
                jumpBufferTimer = jumpBufferTime;
                Debug.Log("Jump buffered");
            }
        }

        // Check for release of jump button
        if (Input.GetKeyUp(KeyCode.Space) && isJumping){
            isJumping = false;
            Debug.Log("Not jumping");
        }
    }

    private void FixedUpdate(){
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        isTouchingWallleft = IsTouchingWallleft();
        isTouchingWallright = IsTouchingWallright();
        isGrounded = IsGrounded();

        if (isTouchingWallleft && !isGrounded || isTouchingWallright && !isGrounded){
            // if touching wall on the right you can only move left and vice versa
            if (isTouchingWallright && moveX > 0){
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }
            if (isTouchingWallleft && moveX < 0){
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }


            // if ((moveX > 0) || (moveX < 0 ))
            // {
            //     //if velocity is going down, then set it to 0 but you can still move left or right depending if the wall is on the left or right
            //     if (rb.velocity.y < 0)
            //     {
                    
            //         rb.velocity = new Vector2(0, rb.velocity.y);
            //         Debug.Log("wallsliding");
            //         return;
            //     }
            //     return;
                
            // }
        }
        
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // Jumping
        if (isJumping && Time.time < jumpTime){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded(){
        float extraHeight = 0.02f;
        Vector2 castPosition = coll.bounds.center + new Vector3(0f, -0.1f, 0f); // Adjust the cast position downward
        RaycastHit2D hit = Physics2D.CapsuleCast(castPosition, coll.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, extraHeight, groundLayer);
        return hit.collider != null;
    }


    private bool IsTouchingWallright(){
        float extraWidth = 0.03f;
        // RaycastHit2D hitRight = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, extraWidth, wallLayer);
        //to capsule collider
        RaycastHit2D hitRight = Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.right, extraWidth, wallLayer);
        return hitRight.collider != null;

    }

    private bool IsTouchingWallleft(){
        float extraWidth = 0.03f;
        // RaycastHit2D hitLeft = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, extraWidth, wallLayer);
        //to capsule collider
        RaycastHit2D hitLeft = Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.left, extraWidth, wallLayer);
        return hitLeft.collider != null;
    }
}