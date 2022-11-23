using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMotor2 : MonoBehaviour
{
    public float MovementSpeed;             //Player's movement speed(velocity)
    private Rigidbody2D rb;                 //Player's Rigidbody
    private bool isGrounded = true;         //Player touches the ground
    public float JumpLength;                //How long the player can jump
    public float JumpForce;                 //How fast the player jumps(velocity)
    private float JumpElapsedTime = 0;      //Elapsed time since jumping
    private bool isJumping = false;         //Player is in the jumping state
    public Transform Feet;                  //Location of the player's feet
    public Transform Head;                  //Location of the player's head
    public BoxCollider2D FeetDetectionBox;  //Hitbox of feet
    public BoxCollider2D HeadDetectionBox;  //Hitbox of head
    public LayerMask layerMask;             //Layer of the ground or standable objects
    private Animator animator;              //Animator of object
    GameObject weaponholder;

    /**
     * Start function of motor
     */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponentInChildren<Animator>();
        weaponholder = GameObject.Find("WeaponHolder");
    }

    /**
     * Moves the player on the x axis
     * @param float inp Input of the player.
     */
    public void Move(float inp)
    {
        float Xspeed = inp * MovementSpeed * Time.deltaTime;
        rb.velocity = new Vector2(Xspeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(Xspeed));
    }

    /**
     * Update function of motor
     * Updates the variable of isJumping, CheckGrounded
     */
    private void Update()
    {
        StartCoroutine(CheckGrounded());

        if (Physics2D.OverlapBox(Head.position, HeadDetectionBox.size, 0, layerMask))
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    /**
     * Starts a jump if the player is grounded
     */
    public void StartJump()
    {
        if (isGrounded)
        {
            isJumping = true;
            JumpElapsedTime = 0;
        }
    }

    /**
     * Continues a jump if player is still in the jumping state,
     * until it reaches the maximum time
     */
    public void KeepJump()
    {
        if (isJumping)
        {
            if (JumpElapsedTime <= JumpLength)
            {
                JumpElapsedTime += Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
            else
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    /**
     * Checks if the player ison the ground
     * Adds a bit of delay for gameplay reasons
     */
    private IEnumerator CheckGrounded()
    {

        bool lastGrounded = isGrounded;
        bool currentGrounded = Physics2D.OverlapBox(Feet.position, FeetDetectionBox.size, 0, layerMask);
        if (!currentGrounded && lastGrounded)
        {
            yield return new WaitForSeconds(0.02f);
        }
        isGrounded = currentGrounded;
        animator.SetBool("IsGrounded", currentGrounded);
    }

    /**
     * Stops the jump, resets the falling speed
     */
    public void StopJump()
    {
        isJumping = false;
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    /**
     * Throws the player in the given direction, player can still move
     */
    public void ThrowPlayer(Vector2 direction, float force)
    {
        rb.velocity = direction.normalized * force;
    }

    /**
     * Returns whether the player is grounded
     * @return bool player is grounded
     */
    public bool IsGrounded()
    {
        return isGrounded;
    }

    /**
     * Mirrors the character
     */
    public void FlipCharacter()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        foreach (Transform child in weaponholder.transform)
        {
            child.localScale = new(child.localScale.x * -1, child.localScale.y * -1, child.localScale.z);
        }
    }
}